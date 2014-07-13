/**
 * Created by Max on 7/13/2014.
 */

( function ($) {
    function getMessageMap(container) {
        return $(container).data('messages');
    }

    function getInputId(container) {
        return $(container).find('[name = "list_id"]');
    }

    function getOutputMessage(container) {
        return $(container).find('[name="message"]');
    }

    // При выборе Id из списка, в текстовое поле вставляется сообщение
    function onChange() {
        var id = getInputId(this).find(':selected').val();
        var selectedMessage = getMessageMap(this).filter( function(message) {
            return message.id == id;
        });
        getOutputMessage(this).val(selectedMessage[0].message);
    }

    var methods = {
        init: function (messageMap) {
            var $container = $(this);
            var $inputId = $('<select name="list_id"/>');
            var $outputMessage = $('<input type="text" disabled name="message"/>').css("width", "200px");

            $container.data("messages", messageMap);

            $container.append('Id:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' +
                              '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;');
            $container.append($inputId);
            $container.append('<br/>Message: ');
            $container.append($outputMessage);
            $container.append('<br/>');
            $container.append('<br/>');

            messageMap.forEach(function (message) {
                $inputId.append('<option value=' + message.id + '>' + message.id + '</option>');
            });

            $inputId.change($.proxy(onChange, this));
            $inputId.change();
            return $container;
        },
        show: function () {
            $(this).children('input').show();
            return $(this);
        },
        hide: function () {
            $(this).children('input').hide();
            return $(this);
        },
        clean: function () {
            $(this).children('input').val('');
            return $(this);
        }
    };

    $.fn.messanger = function( method ) {

        if ( methods[method] ) {
            return methods[method].apply( this, Array.prototype.slice.call( arguments, 1 ));
        } else  {
            return methods.init.apply( this, Array.prototype.slice.call( arguments, 0 ) );
        }
    };
})(jQuery);







