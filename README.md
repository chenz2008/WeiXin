微信公众账号开发核心库
======

### 说明 ###

微信公众账号源码，用 C# 开发；如果您想使用，请Star或Fork，在使用过程中如果您觉得源码写的不好的地方可以贡献您的代码。如果需要看实现功能，可以扫描下面二维码，关注测试账号。注意，服务器也许有一天会挂掉。

![扫一扫二维码](https://raw.githubusercontent.com/WangWenzhuang/WeiXin/master/OR.jpg)

----

### 快速使用 ###

*1、实现 IWeiXinService 接口*

```csharp
	// 首先实现 IWeiXinService 接口
    public class ProcessMessage : IWeiXinService
    {
        /// <summary>
        /// 处理消息的接口，如果回复被动消息，直接回复 xml 格式的消息即可。
        /// 如果不处理或者发送客服消息等直接回复空字符串即可。
        /// </summary>
        /// <param name="receiveXmlMsgType"></param>
        /// <param name="receiveXmlMessage"></param>
        /// <returns></returns>
        public string Process(ReceiveXmlMessageType receiveXmlMsgType, ReceiveXmlMessage receiveXmlMessage)
        {
            string result = string.Empty;
            if (receiveXmlMessage != null)
            {
                // 消息类型
                switch (receiveXmlMsgType)
                {
                    case ReceiveXmlMessageType.Undefined:                          // 未识别出消息类型
                        break;
                    case ReceiveXmlMessageType.Text:                               // 文本消息
                        break;
                    case ReceiveXmlMessageType.Image:                              // 图片消息
                        break;
                    case ReceiveXmlMessageType.Voice:                              // 语音消息
                        break;
                    case ReceiveXmlMessageType.Video:                              // 视频消息
                        break;
                    case ReceiveXmlMessageType.Location:                           // 地理位置消息
                        break;
                    case ReceiveXmlMessageType.Link:                               // 链接消息
                        break;
                    case ReceiveXmlMessageType.Event_QRCode_Subscribe:             // 用户未关注时扫描二维码事件
                        break;
                    case ReceiveXmlMessageType.Event_QRCode_Scan:                  // 用户已关注时扫描二维码事件
                        break;
                    case ReceiveXmlMessageType.Event_View:                         // 点击菜单跳转链接时事件
                        break;
                    case ReceiveXmlMessageType.Event_Click:                        // 点击菜单拉取消息时事件
                        break;
                    case ReceiveXmlMessageType.Event_Location:                     // 上报地理位置时事件
                        break;
                    case ReceiveXmlMessageType.Event_Subscribe:                    // 关注事件
                        break;
                    case ReceiveXmlMessageType.Event_UnSubscribe:                  // 取消关注事件
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
```	

*2、注册实现的接口*

```csharp
	WeiXinService.Register(_Service, appid, appsecret);
```	

*3、处理微信服务器发来的 xml*

```csharp
	WeiXinService.ProcessMessage(xml);
```	

在实现 IWeiXinService 接口的 Process 方法中专注开发你的功能吧。

----

### 更新 ###

#### 2014/7/31 ####

完善：

* 修复创建二维码不成功bug，创建二维码返回信息添加二维码图片url

#### 2014/7/29 ####

实现：

* 将日志记录更改为接口形式，使用时可以根据自己的方式记录日志

更改：

* 重构所有代码，整理所有接口，使其简单易用

完善：

* 被动消息所有消息类型都已实现

#### 2014/7/10 ####

实现：

* 添加 OAuth2.0 授权之后获取用户信息

#### 2014/7/8 ####

实现：

* 添加 OAuth2.0 授权之后获取用户 OpenId

#### 2014/7/1 ####

实现：

* 添加微信接口全局返回码，可以快速定位接口请求错误

更改：

* 改变 AccessToken 存储方式，将原来数据库存储改为内存存储，AccessToken 理论有效时间为2小时，用内存完全可以替代。

完善：

* 接口日志记录更加详细

#### 2014/6/30 ####

实现：

* 创建临时带参数二维码

* 创建永久带参数二维码

* 获取已关注用户列表

* 获取已关注用户基本信息

* 高级群发（根据openid）

#### 2014/6/28 ####

实现：

* 被动文本消息

* 被动图文消息

* 客服文本消息

* 客服图文消息

* 语音识别

* 打开网页

* 利用文本消息打开网页获取OpenId

* 显示地理位置

----

### 作者 ###

> [王文壮](https://github.com/wangwenzhuang)

>[naslover](https://github.com/naslover)