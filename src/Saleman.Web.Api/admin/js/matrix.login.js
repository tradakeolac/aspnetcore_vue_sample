
$(document).ready(function(){

	var login = $('#loginform');
	var recover = $('#recoverform');
    var speed = 400;

    var tokenStorage = new TokenStorage();

	$('#to-recover').click(function(){
		
		$("#loginform").slideUp();
		$("#recoverform").fadeIn();
	});
	$('#to-login').click(function(){
		
		$("#recoverform").hide();
		$("#loginform").fadeIn();
	});
	

    login.submit(function (e) {
        e.preventDefault();
        $.post('http://localhost:5001/connect/token', $('#loginform').serialize(), function (data) {
            if (data) {
                tokenStorage.storeToken(data);

                window.location.href = 'http://localhost:5001/Admin/';
            }
        })
	});
    
    if($.browser.msie == true && $.browser.version.slice(0,3) < 10) {
        $('input[placeholder]').each(function(){ 
       
        var input = $(this);       
       
        $(input).val(input.attr('placeholder'));
               
        $(input).focus(function(){
             if (input.val() == input.attr('placeholder')) {
                 input.val('');
             }
        });
       
        $(input).blur(function(){
            if (input.val() == '' || input.val() == input.attr('placeholder')) {
                input.val(input.attr('placeholder'));
            }
        });
    });       
    }
});