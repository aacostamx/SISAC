/**
 * @author David Graham <prograhammer@gmail.com>
 * @version v1.0.0
 * @link https://github.com/prograhammer/bootstrap-table-contextmenu
 */

!function($) {

    'use strict';

    $.extend($.fn.bootstrapTable.defaults, {
        contextMenu: undefined,
        contextMenuTrigger: 'right',
        contextMenuAutoClickRow: true,
        contextMenuButton: undefined,
        onContextMenuItem: function (row, $element) {
            return false;
        },
        onContextMenuRow: function (row, $element) {
            return false;
        }
    });

    $.extend($.fn.bootstrapTable.Constructor.EVENTS, {
        'contextmenu-item.bs.table': 'onContextMenuItem',
        'contextmenu-row.bs.table': 'onContextMenuRow'
    });

    var BootstrapTable = $.fn.bootstrapTable.Constructor,
        _initBody = BootstrapTable.prototype.initBody;

    BootstrapTable.prototype.initBody = function () {

        // InitBody
        _initBody.apply(this, Array.prototype.slice.apply(arguments));

        if (this.options.contextMenu) {
            initContextMenu(this);
        }
    };

    var initContextMenu = function (el) {
        var that = el;

        // Context menu on Right-click
        if (that.options.contextMenuTrigger == 'right' || that.options.contextMenuTrigger == 'both') {
            that.$body.find('> tr[data-index]').off('contextmenu').on('contextmenu', function (e) {
                var $tr = $(this);
                showContextMenu(that, $tr, e.clientX, e.clientY);
                return false;
            });
        }

        // Context menu on Left-click
        if (that.options.contextMenuTrigger == 'left' || that.options.contextMenuTrigger == 'both') {
            that.$body.find('> tr[data-index]').off('click').on('click', function (e) {
                var $tr = $(this);
                showContextMenu(that, $tr, e.clientX, e.clientY);
                return false;
            });
        }

        // Context menu on Button-click
        if (typeof that.options.contextMenuButton === 'string') {
            that.$body.find('> tr[data-index]').find(that.options.contextMenuButton).off('click').on('click', function (e) {
                var $tr = $(this).closest('tr[data-index]');
                showContextMenu(that, $tr, $(this)[0].getBoundingClientRect().left, $(this)[0].getBoundingClientRect().bottom);
                return false;
            });
        }

        if (typeof that.options.contextMenu === 'string') {
            var $menu = $(that.options.contextMenu);

            // Click on context menu item
            var $row = that.$body.find('tr');
            $menu.find('li').off('click').on('click', function (e) {
                var rowData = that.data[$menu.data('index')];
                that.trigger('contextmenu-item', rowData, $(this));
            });

            // Left click anywhere outside to hide context menu
            $(document).click(function () {
                $menu.hide();
            });
        }

    }

    var showContextMenu = function (el, $tr, screenPosX, screenPosY){
        var that = el,
            item = that.data[$tr.data('index')];

        function getMenuPosition($menu, screenPos, direction, scrollDir) {
            var win = $(window)[direction](),
                scroll = $(window)[scrollDir](),
                menu = $menu[direction](),
                position = screenPos + scroll;

            if (screenPos + menu > win && menu < screenPos)
                position -= menu;

            return position;
        };

        if (typeof that.options.contextMenu === 'string') {
            var $menu = $(that.options.contextMenu);
            $menu.data('index', $tr.data('index'))
                .appendTo($('body'))
                .css({
                    position: "absolute",
                    left: getMenuPosition($menu, screenPosX, 'width', 'scrollLeft'),
                    top: getMenuPosition($menu, screenPosY, 'height', 'scrollTop'),
                    zIndex: 1100
                })
                .show();
        }

        that.trigger('contextmenu-row', item, $tr);

        if(that.options.contextMenuAutoClickRow && that.options.contextMenuTrigger == 'right') {
            that.trigger('click-row', item, $tr);
        }
    };


}(jQuery);