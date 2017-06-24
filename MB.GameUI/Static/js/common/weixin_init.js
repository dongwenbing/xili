var weixinapi = {}

weixinapi.data = null;
weixinapi.load = function (data)
{
    wx.config({
        debug: false,
        appId: data.appid,
        timestamp:data.timestamp,
        nonceStr: data.noncestr,
        signature: data.signature,
        jsApiList: ['onMenuShareTimeline','onMenuShareAppMessage','onMenuShareQQ','onMenuShareWeibo','onMenuShareQZone','startRecord','stopRecord','onVoiceRecordEnd','playVoice','pauseVoice','stopVoice','onVoicePlayEnd','uploadVoice','downloadVoice','chooseImage','previewImage','uploadImage','downloadImage','translateVoice','getNetworkType','openLocation','getLocation','hideOptionMenu','showOptionMenu','hideMenuItems','showMenuItems','hideAllNonBaseMenuItem','showAllNonBaseMenuItem','closeWindow','scanQRCode','chooseWXPay','openProductSpecificView','addCard','chooseCard','openCard'] 
    });

    wx.ready(function () {
        wx.onMenuShareQQ({
            title: '测试分享到qq', // 分享标题
            desc: '我只是想测试以下,右边是自定义图片', // 分享描述
            link: 'http://game.dfmeibao.mobi/Weixin/Demo', // 分享链接
            imgUrl: 'http://www.dfmeibao.com.cn/default/images/expand_icon.jpg', // 分享图标
            success: function () {
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });
        wx.onMenuShareQZone({
            title: '测试分享到qq空间', // 分享标题
            desc: '我只是想测试以下,右边是自定义图片', // 分享描述
            link: 'http://game.dfmeibao.mobi/Weixin/Demo', // 分享链接
            imgUrl: 'http://www.dfmeibao.com.cn/default/images/expand_icon.jpg', // 分享图标
            success: function () {
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });
        wx.onMenuShareWeibo({
            title: '测试分享到腾讯微博', // 分享标题
            desc: '我只是想测试以下,右边是自定义图片', // 分享描述
            link: 'http://game.dfmeibao.mobi/Weixin/Demo', // 分享链接
            imgUrl: 'http://www.dfmeibao.com.cn/default/images/expand_icon.jpg', // 分享图标
            success: function () {
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });
        wx.onMenuShareTimeline({
            title: '测试分享到微信朋友圈', // 分享标题
            desc: '我只是想测试以下,右边是自定义图片', // 分享描述
            link: 'http://game.dfmeibao.mobi/Weixin/Demo', // 分享链接
            imgUrl: 'http://www.dfmeibao.com.cn/default/images/expand_icon.jpg', // 分享图标
            success: function () {
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });
        wx.onMenuShareAppMessage({
            title: '测试分享到微信聊天窗口', // 分享标题
            desc: '我只是想测试以下,右边是自定义图片', // 分享描述
            link: 'http://game.dfmeibao.mobi/Weixin/Demo', // 分享链接
            imgUrl: 'http://www.dfmeibao.com.cn/default/images/expand_icon.jpg', // 分享图标
            type: '', // 分享类型,music、video或link，不填默认为link
            dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
            success: function () {
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                // 用户取消分享后执行的回调函数
            }
        });
    });

   //高薪四道  ui画册，  png  rng 都比都比  

    wx.error(function (res) {
        console.log("微信jssdk验证失败");
    });
    //以下开始对各功能封装
    weixinapi.scanQRCode = function ()
    {
        wx.scanQRCode({
            needResult: 0, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
            scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
            success: function (res) {
                var result = res.resultStr; // 当needResult 为 1 时，扫码返回的结果
          

            }
        });

    }
    weixinapi.chooseImage = function ()
    {
        wx.chooseImage({
            count: 1, // 默认9
            sizeType: ['original', 'compressed'], // 可以指定是原图还是压缩图，默认二者都有
            sourceType: ['album', 'camera'], // 可以指定来源是相册还是相机，默认二者都有
            success: function (res) {
                var localIds = res.localIds; // 返回选定照片的本地ID列表，localId可以作为img标签的src属性显示图片
            }
        });
    }
    weixinapi.getNetworkType = function () {
    wx.getNetworkType({
        success: function (res) {
            var networkType = res.networkType; // 返回网络类型2g，3g，4g，wifi.
            alert('你的手机网络是'+networkType);
        }
    });
    }
    weixinapi.getLocation = function ()
    {
        wx.getLocation({
            type: 'gcj02', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
            success: function (res) {
                var latitude = res.latitude; // 纬度，浮点数，范围为90 ~ -90
                var longitude = res.longitude; // 经度，浮点数，范围为180 ~ -180。
                var speed = res.speed; // 速度，以米/每秒计
                var accuracy = res.accuracy; // 位置精度
                alert('你的位置是' + 'latitude:' + latitude + 'longitude:' + longitude);
               
            }
        });
    }
    weixinapi.openLocation = function ()
    {
        wx.openLocation({
            latitude: 39.897445, // 纬度，浮点数，范围为90 ~ -90
            longitude: 116.331398, // 经度，浮点数，范围为180 ~ -180。
            name: '牛逼哄哄的北京', // 位置名
            address: '南山区深南大道', // 地址详情说明
            scale: 11, // 地图缩放级别,整形值,范围从1~28。默认为最大
            infoUrl: 'www.dfmeibao.com' // 在查看位置界面底部显示的超链接,可点击跳转
        });
    }
    weixinapi.showMenuItems = function ()
    {
        wx.showMenuItems({
            menuList: ['menuItem:share:appMessage', 'menuItem:share:qq'] // 要显示的菜单项，所有menu项见附录3
            //举报: "menuItem:exposeArticle"
            //调整字体: "menuItem:setFont"
            //日间模式: "menuItem:dayMode"
            //夜间模式: "menuItem:nightMode"
            //刷新: "menuItem:refresh"
            //查看公众号（已添加）: "menuItem:profile"
            //查看公众号（未添加）: "menuItem:addContact"
            //传播类
            //发送给朋友: "menuItem:share:appMessage"
            //分享到朋友圈: "menuItem:share:timeline"
            //分享到QQ: "menuItem:share:qq"
            //分享到Weibo: "menuItem:share:weiboApp"
            //收藏: "menuItem:favorite"
            //分享到FB: "menuItem:share:facebook"
            //分享到 QQ 空间/menuItem:share:QZone
            //保护类
            //编辑标签: "menuItem:editTag"
            //删除: "menuItem:delete"
            //复制链接: "menuItem:copyUrl"
            //原网页: "menuItem:originPage"
            //阅读模式: "menuItem:readMode"
            //在QQ浏览器中打开: "menuItem:openWithQQBrowser"
            //在Safari中打开: "menuItem:openWithSafari"
            //邮件: "menuItem:share:email"
            //一些特殊公众号: "menuItem:share:brand"
        });


    }
    weixinapi.hideMenuItems = function () {
        wx.hideMenuItems({
            menuList: ['menuItem:share:appMessage', 'menuItem:share:qq'] // 要隐藏的菜单项，只能隐藏“传播类”和“保护类”按钮，所有menu项见附录3
        });
    }
    weixinapi.hideAllNonBaseMenuItem=function()
    {
        wx.hideAllNonBaseMenuItem();//隐藏所有非基础类按钮{
    }
    weixinapi.showAllNonBaseMenuItem = function ()
    {
        wx.showAllNonBaseMenuItem();//显示所有非基础类按钮

    }
    weixinapi.onMenuShareAppMessage = function ()
    {
        wx.onMenuShareAppMessage({
            title: '测试分享', // 分享标题
            desc: '你想说点什么', // 分享描述
            link: '/Weixin/Demo', // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
            imgUrl: 'http://www.dfmeibao.com.cn/default/images/expand_icon.jpg', // 分享图标
            type: 'link', // 分享类型,music、video或link，不填默认为link
            dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
            success: function () {
                alert('成功');
                // 用户确认分享后执行的回调函数
            },
            cancel: function () {
                alert('取消');
                // 用户取消分享后执行的回调函数
            }
        });
        // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。



    }
    weixinapi.showAllNonBaseMenuItem = function ()
    {
        wx.showAllNonBaseMenuItem();//显示所有非基础功能按钮
    }

}


