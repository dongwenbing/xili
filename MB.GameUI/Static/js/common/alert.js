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

        //	alert("���ֶ�ѡ����������(��Ctrl+C����)d\n\r"+copyText);
        var dom='<div class="pc-body"><div  class="pc-alert-box" id="wenbing">���ֶ�ѡ����������(��Ctrl+C���ƣ�</br><span>'+copyText+'</span><div class="closebutton" onclick="pcalert.close()">�ر�</div></div>';
        pcalert.alert(dom);
}