var userinfo = {}

userinfo.app = new Vue({
    el: '#app',
    data: {
        message: '微信js接口实例页!'
    },
    methods: {
        getdata: function () {
            this.$http.get("/Weixin/WeixinSdk").then(function (rtdata) {
                if (!rtdata) return false;
                rtdata = eval("(" + rtdata.body + ")");
                weixinapi.load(rtdata);
                return rtdata;
            },
                function (error) { })
        }
    }
})

//userinfo.app.getdata();

