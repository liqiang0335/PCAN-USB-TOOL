v1.0
1. 创建网关检测界面。功能包括：创建和设定多路CAN设备字典；多路设备同时启动；多路设备手动停止和关闭窗口自动停止；清空输出日志；终止检测线程；创建和开启检测线程；任务进度条展示。  
2. 改写SetCANParam界面。改动包括：添加构造方法重载以支持多路CAN设备字典的创建和同时开启；添加适用于多路CAN设备同时初始化时使用的“应用”按钮及其点击事件；添加原始界面与网关检测界面设置参数时按钮动态变化的效果。  
3. 创建网关检测任务类对象。功能包括：按照正则表达式判断网关检测界面发送ID范围文本框所写内容合法性，计算发送ID集合，判断ID集合合法性；创建新线程开启检测任务，检测线程方法实体；结果显示和进度显示的委托方法；检测线程的终止方法。  
4. 改写USBCAN类对象。改动包括：添加返回已连接设备数目的静态方法框架；向类中添加设备号及波特率两个动态成员变量；添加接收线程对数据展示委托是否存在的判断以及与类对象绑定的数据接收缓冲，最大长度为10w。  
5. 改写BasicFunction类。改动包括：添加网关检测界面进入菜单。

v1.1
1. 对应canfd底层驱动调试完毕的版本。  
2. 添加网关检测界面桑基图展示，基于百度echart的js脚本实现。  
3. 删减部分多余的打印输出。  