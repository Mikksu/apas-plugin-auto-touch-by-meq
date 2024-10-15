# APAS插件开发指南

## 工作原理

APAS主程序通过`ISystemServiceContract`接口向插件提供设备控制的方法。

插件接口`IPluginsContract`定义了一组函数和属性，用于约定APAS加载插件后有哪些方法可以用来操作插件。同时，IPluginsContract接口通过ISystemServiceContract接口，对APAS系统中的各种功能进行调用。

最新版本的APAS系统支持两种插件类型：

1. 设备控制类
2. 图像处理类

APAS主程序启动时，按以下顺序加载插件：

1. 遍历`Plugins`目录下的所有dll文件，并加载这些dll。
2. 检查dll中封装的对象是否继承`IPlugin`接口。
3. 检查dll所在的目录，如果是`Equipment`，则用`PluginEquipment`实例化对象；如果是`ImageProcess`，则用`PluginImageProcess`实例化对象。

## 使用模板
