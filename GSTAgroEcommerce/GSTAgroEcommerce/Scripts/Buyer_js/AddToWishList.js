$(document).ready(function () {


    $(".addtowishlist").click(function () {
        var $buttonClicked = $(this);
        var json = $buttonClicked.attr('data-id')
        var parent = $buttonClicked.parent().parent().parent().parent();
        var Quantity = parent.find(".Quantity").text();

        $.ajax({
            type: 'POST',
            url: "/Buyer/AddToWishList",
            dataType: "json",
            /* data: { "jsonproductcode": JSON.stringify(json) },*/
            data: { "jsonproductcode": json, "qty": Quantity },
            success: function (data) {
                if (data.status == "true") {
                    // alert(data.msg);
                    swal({
                        title: data.msg,
                        icon: "success",
                        button: "Ok",
                    });
                    parent.find(".addtowishlist").css("color", "red");

                }
                if (data.status == "false") {

                    swal({
                        title: data.msg,
                        icon: "success",
                        button: "Ok",
                    });
                    parent.find(".addtowishlist").css("color", "white");
                }
            },
            error: function (result) {
                swal({
                    title: "You Need To Login",
                    icon: "warning",
                    button: "Ok",
                }).then(function () {
                    window.location.href = "/Buyer/Login";
                })

                //sweetAlert({
                //    title: "You Need To Login",
                //    icon: "warning",
                //    button: "Ok",

                //})
                //    .than(function () {
                //        window.location.href = "/Buyer/Login";

                //    });

                /* var Loginurl = "@Url.Action("Login", "Buyer")";//Redirect to loginpage*/
                //var Loginurl = "/Buyer/Login";//Redirect to loginpage
                //window.location.href = Loginurl;
                /* window.location.reload();*/

            }
        });
    });
});
/*parent.find(".red").css('color', 'red');*/