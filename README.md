# 今日关键词云图片
抓取今日关键字，并生成云图

- 逛街的时候看到这新闻聚合做的挺好 https://tools.miku.ac/news/ ，省了我一个一个的去翻排行榜。于是手痒随手把新闻的关键词给提取出来做成一个词云图片。
- 程序员不要只一心撸码，偶尔也可以每天关注一下世界变化嘛。

#### 效果如下（图片存在ipfs图床，能不能看到随缘）
![https://bafybeigenijlvvnsd5iq3xaus26f2c4iqvumgohq2fbfz2hiukif2wnetu.ipfs.cf-ipfs.com/](https://bafybeigenijlvvnsd5iq3xaus26f2c4iqvumgohq2fbfz2hiukif2wnetu.ipfs.cf-ipfs.com/)

#### TIP
如果你需要调试，需要安装一个100版本的chrome，因为chrome driver 是下载的100版本的 https://registry.npmmirror.com/binary.html?path=chromedriver/100.0.4896.60/。

#### PS

如果程序调试完后发现网络挂了，请检查系统的代理服务器设置，因为有时候会用力过猛，收不回来。

想到这东西还能干点啥么？欢迎联系 https://t.me/Odinluo

### mac 版本怎么玩

mac 的开发工具还是建议使用 vs studio 2022，碰到问题调试起来会更容易一些。

----

因为程序是利用代理的模式去抓取网站的通讯，所以只要你打开了被抓取的网站就可以获取到关键字了。
暂时没时间去调试浏览器证书环保什么的，所以只想测试一下的mac用户请这么玩：

- 安装一个骚气的简化版浏览器，在这里我们推荐 https://menubarx.app/ 
- 系统偏好 => 网络 => (当前激活的网络)高级 => 代理 =>
- 先设置代理，默认为 127.0.0.1 8000 ， 将http 和 https 的代理都设置为这个，点右下角按钮（好），
- 右下角按钮=>应用。
- 打开release 版本的程序或者进入调试，run
  - 终端运行 dotnet WordCloud.Proxy.dll
- 使用你的简化版浏览器，访问 https://tools.miku.ac/news/ 
- 回到你的终端，等待程序执行完成
- 进入你的程序目录，查看最新生成的词云图片吧
- 记得把你的代理设置改回去哟。

# Today.WordCloud
automatic create word cloud image with Chinese NEWS keywords in today 

#### News from 
https://tools.miku.ac/news/

#### chrome file download from
https://registry.npmmirror.com/binary.html?path=chromedriver/100.0.4896.60/

#### Contact

if you have any question , please contact https://t.me/Odinluo



