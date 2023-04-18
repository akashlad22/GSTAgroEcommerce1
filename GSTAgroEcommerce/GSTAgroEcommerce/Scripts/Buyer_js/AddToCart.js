$(document).ready(function () {

    $(".AddToCart").click(function () {
        var Jsonproductcode = $(this).attr("data-id");

        $.ajax({
            type: "POST",
            dataType: "json",
            data: { "productcode": Jsonproductcode },
            url: "/Buyer/AddToCart",
            success: function (data) {
                if (data.status == "New") {
                    swal({
                        title: data.msg,
                        icon: "success",
                        button: "Ok",
                    }).then(function () {
                        window.location.href = "/Buyer/WishListGrid";
                    })



                }
               
                if (data.status == "true") {
                    swal({
                        title: data.msg,
                        icon: "success",
                        button: "Ok",
                    }).then(function () {
                        window.location.href = "/Buyer/WishListGrid";
                    })

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
                //swal({
                //    title: "You need To LogIn",
                //    icon: "success",
                //    button: "Ok",
                //    timer: 800000000000000000,
                //    showConfirmButton: true
                //});
               
                //var LoginURl = "/Buyer/LogIn"; 
                //window.location.href = LoginURl; 

            }

        });
    });
});