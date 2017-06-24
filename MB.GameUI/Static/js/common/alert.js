var pcalert={}
pcalert.alert=function(dom)
{
        var bgObj=document.createElement("div");
        bgObj.setAttribute('id','AlertDiv');
        bgObj.innerHTML=dom;
        document.body.appendChild(bgObj);

}
pcalert.close=function()
{
        var deldom=document.getElementById("AlertDiv");
        deldom.parentNode.removeChild(deldom)
}
function copy_code(copyText) {

        //	alert("请手动选择以下内容(按Ctrl+C复制)d\n\r"+copyText);
        var dom='<div class="pc-body"><div  class="pc-alert-box" id="wenbing">请手动选择以下内容(按Ctrl+C复制）</br><span>'+copyText+'</span><div class="closebutton" onclick="pcalert.close()">关闭</div></div>';
        pcalert.alert(dom);
}