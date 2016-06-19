var oTable = null;
var initDataTables = function () {
    var tableOptions; // main options
    var dataTable; // datatable object
    var table; // actual table jquery object
    var the;
    function retrieveData(sSource, aoData, fnCallback) {
        /* get 方法调用*/
        $.ajax({
            "type": "get",
            "contentType": "application/json",
            "url": sSource,
            "dataType": "json",
            "data": aoData,
            "success": function (resp) {//服务器端返回的对象的returnObject部分是要求的格式
                if (resp.success)
                    fnCallback(resp.result);
                else
                    fnCallback(resp);
            }
        });
        /* Post 方法调用
        $.ajax({
          "type": "post",
          "url": sSource,
          "dataType": "json",
          "data": aoData,
          "success": function (resp) {
            fnCallback(resp); //服务器端返回的对象的returnObject部分是要求的格式
          }
        });*/
    }
    return {
        init: function (options) {
            if (!$().dataTable)
                return;
            the = this;
            options = $.extend(true, {
                src: "",
                dataTable: {
                    "bLengthChange": false, //改变每页显示数据数量
                    "bFilter": false, //过滤功能
                    "bProcessing": true, //开启读取服务器数据时显示正在加载中……特别是大数据量的时候，开启此功能比较好
                    "bServerSide": true, //开启服务器模式，使用服务器端处理配置datatable。注意：sAjaxSource参数也必须被给予为了给datatable源代码来获取所需的数据对于每个画。 这个翻译有点别扭。
                    "iDisplayLength": 20, //每页显示10条数据
                    //ajax地址
                    "sAjaxSource": "", // get地址
                    //"sAjaxSource": "/Home/Home/UserListPost",// post地址
                    "fnServerData": retrieveData, //执行方法
                    "bSort": false,
                    //默认排序
                    "order": [
                        [0, 'asc'] //第一列正序
                    ],
                    "lengthMenu": [
                        [5, 15, 20, -1],
                        [5, 15, 20, "All"] // change per page values here
                    ],
                    // set the initial value
                    "pageLength": 10,
                    ////向服务器传额外的参数
                    //"fnServerParams": function(aoData) {
                    //    aoData.push(
                    //    { "name": "UserName", "value": $('#s_searchtype').val() }, //员工名字
                    //    { "name": "Division", "value": $('#content').val() }); //所处Division
                    //},
                    //配置列要显示的数据
                    //columns: [
                    //    //对应上面thead里面的序列 ;字段名字和返回的json序列的key对应
                    //    { "data": "account" },
                    //    { "data": "account" },
                    //    { "data": "name" },
                    //    { "data": "role_id" },
                    //    { "data": "org_id" },
                    //    { "data": "create_time" },
                    //    { "data": "status" },
                    //    { "data": "creater" }
                    //],
                    //按钮列
                    //"columnDefs": [
                    //    {
                    //        "render": function(data, type, row, me) {
                    //            return me.row + 1;
                    //        },
                    //        "targets": 0
                    //    },
                    //    {
                    //        "render": function(data, type, row) {
                    //            if (!data) {
                    //                return '';
                    //            } else {
                    //                data = data.replace("/Date(", "").replace(")/", "");
                    //                var time = new Date(parseInt(data));
                    //                var year = time.getFullYear();
                    //                var month = time.getMonth() + 1;
                    //                var date = time.getDate();
                    //                var hour = time.getHours();
                    //                var minute = time.getMinutes();
                    //                var second = time.getSeconds();
                    //                return year + "-" + month + "-" + date + "   " + hour + ":" + minute + ":" + second;
                    //            }
                    //        },
                    //        'orderable': false,
                    //        "targets": 5
                    //    },
                    //    {
                    //        "render": function(data, type, row) {
                    //            var opt = "";
                    //            opt += '<a href="#" class="btn default btn-xs green" title="编辑"><i class="fa fa-edit"></i>编辑</a>';
                    //            opt += '<a href="#" class="btn default btn-xs green" title="查看详情"><i class="fa fa-eye"></i>查看详情</a>';
                    //            return opt;
                    //        },
                    //        'orderable': false,
                    //        "targets": 8
                    //    }
                    //],
                    //语言配置--页面显示的文字
                    "language": {
                        "oAria": {
                            "sSortAscending": ": 升序排列",
                            "sSortDescending": ": 降序排列"
                        },
                        "oPaginate": {
                            "sFirst": "首页",
                            "sLast": "末页",
                            "sNext": "下页",
                            "sPrevious": "上页"
                        },
                        "sEmptyTable": "没有相关记录",
                        "sInfo": "第 _START_ 到 _END_ 条记录，共 _TOTAL_ 条",
                        "sInfoEmpty": "第 0 到 0 条记录，共 0 条",
                        "sInfoFiltered": "(从 _MAX_ 条记录中检索)",
                        "sInfoPostFix": "",
                        "sDecimal": "",
                        "sThousands": ",",
                        "sLengthMenu": "每页显示条数: _MENU_",
                        "sLoadingRecords": "正在载入...",
                        "sProcessing": "正在载入...",
                        "sSearch": "搜索:",
                        "sSearchPlaceholder": "",
                        "sUrl": "",
                        "sZeroRecords": "没有相关记录"
                    }
                }
            }, options);

            tableOptions = options;
            // create table's jquery object
            table = $(options.src);

            // initialize a datatable
            dataTable=table.DataTable(options.dataTable);
        },
        submitFilter: function() {
            dataTable.ajax.reload();
        }
    }
};
