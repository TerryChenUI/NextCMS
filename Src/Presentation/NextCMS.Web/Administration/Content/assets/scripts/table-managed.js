//JQuery DataTable
var TableManaged = {
    oTable: '',
    iDisplayLength : 15,

    init: function (dataTableObj, actionUrl, aoColumns, pageSize) {
        
        if (!jQuery().dataTable) {
            return;
        }

        if (typeof (pageSize) != 'undefined') {
            this.iDisplayLength = pageSize;
        }

        this.oTable = dataTableObj.dataTable({
            'bLengthChange': false,
            "bFilter": false,
            "bSort": false,
            "bPaginate": true,
            "bServerSide": true,
            //"bStateSave": true,
            "iDisplayLength": this.iDisplayLength,
            "sPaginationType": "bootstrap",
            "oLanguage": {
                "sLengthMenu": "每页显示 _MENU_ 条记录",
                "sZeroRecords": "对不起，查询不到任何相关数据",
                "sInfo": "当前显示 _START_ 到 _END_ 条，共 _TOTAL_ 条记录",
                "sInfoEmtpy": "找不到相关数据",
                "sInfoFiltered": "数据表中共为 _MAX_ 条记录",
                "sProcessing": "正在加载中...",
                "sSearch": "搜索",
                "oPaginate": {
                    "sFirst": "第一页",
                    "sPrevious": " 上一页 ",
                    "sNext": " 下一页 ",
                    "sLast": " 最后一页 "
                }
            },
            "sAjaxSource": actionUrl,
            "aoColumns": aoColumns,
            "aoColumnDefs": [{
                'bSortable': false,
                'aTargets': [0]
            }
            ]
        });

        //// begin: third table
        //$('#sample_3').dataTable({
        //    "aLengthMenu": [
        //        [5, 15, 20, -1],
        //        [5, 15, 20, "All"] // change per page values here
        //    ],
        //    // set the initial value
        //    "iDisplayLength": 5,
        //    "sPaginationType": "bootstrap",
        //    "oLanguage": {
        //        "sLengthMenu": "_MENU_ records",
        //        "oPaginate": {
        //            "sPrevious": "Prev",
        //            "sNext": "Next"
        //        }
        //    },
        //    "aoColumnDefs": [{
        //            'bSortable': false,
        //            'aTargets': [0]
        //        }
        //    ]
        //});

        //jQuery('#sample_3 .group-checkable').change(function () {
        //    var set = jQuery(this).attr("data-set");
        //    var checked = jQuery(this).is(":checked");
        //    jQuery(set).each(function () {
        //        if (checked) {
        //            $(this).attr("checked", true);
        //        } else {
        //            $(this).attr("checked", false);
        //        }
        //    });
        //    jQuery.uniform.update(set);
        //});

        //jQuery('#sample_3_wrapper .dataTables_filter input').addClass("form-control input-small"); // modify table search input
        //jQuery('#sample_3_wrapper .dataTables_length select').addClass("form-control input-xsmall"); // modify table per page dropdown
        //jQuery('#sample_3_wrapper .dataTables_length select').select2(); // initialize select2 dropdown
    },

    search: function (searchUrl) {
        var filterdata = $(".search-form").serialize();
        var actionUrl = searchUrl + "?rand=" + Math.random() + "&" + filterdata;
        this.oTable.fnReloadAjax(actionUrl);
    },

    reset:function(){
        $(".search-form").find(':input').not(':button, :submit, :reset').val('').removeAttr('checked').removeAttr('selected');
    },

    btnEdit: function (actionUrl) {
        return "<a href=\"" + actionUrl + "\" class=\"btn btn-xs purple\"><i class=\"fa fa-edit\"></i> 编辑</a>";
    },

    btnDelete: function (actionUrl) {
        return "<a href=\"javascript:void(0)\" class=\"btn btn-xs red\" onclick=\"TableManaged.confirmDelete(this, '" + actionUrl + "')\"><i class=\"fa fa-times\"></i> 删除</a>";
    },

    confirmDelete: function (target, actionUrl) {
        if (!confirm("你确认要删除此记录？")) {
            return;
        }

        $.ajax({
            type:"POST",
            url: actionUrl,
            success: function (response) {
                if (response.success) {
                    var nRow = $(target).parents('tr')[0];
                    TableManaged.oTable.fnDeleteRow(nRow);
                } else {
                    alert(response.message);
                }
                
            }
        })

    }
};

$('.datatable .group-checkable').change(function () {
    var set = jQuery(this).attr("data-set");
    var checked = jQuery(this).is(":checked");
    jQuery(set).each(function () {
        if (checked) {
            $(this).attr("checked", true);
        } else {
            $(this).attr("checked", false);
        }
    });
    jQuery.uniform.update(set);
});