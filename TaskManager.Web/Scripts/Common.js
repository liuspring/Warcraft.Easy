//初始化fileinput控件（第一次初始化）
var initFileInput = function (ctrlName, uploadUrl, allowedFileExtensions) {
    var control = $('#' + ctrlName);
    control.fileinput({
        language: 'zh', //设置语言
        uploadUrl: uploadUrl, //上传的地址
        allowedFileExtensions: allowedFileExtensions, //接收的文件后缀
        showUpload: true, //是否显示上传按钮,
        showCaption: false, //是否显示标题
        browseClass: "btn btn-primary", //按钮样式             
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
    });
}

$(function () {



});
