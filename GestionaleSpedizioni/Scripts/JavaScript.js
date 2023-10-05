

$(document).ready(function () {

    $("#btn1").click(function () {
    $("#oggi").slideToggle();
        $("#lista").empty();
        $.ajax({
            method: "GET",
            url: "ConsegnaOggi",
            success: function (sp) {
                console.log(sp);
                $.each(sp, function (i, c) {

                    var list = `<li> Numero Spedizione:  ${c.IdSpedizione}  Destinazione: ${c.Destinazione} </li>`
                    $("#lista").append(list);

                })

            }
        })
    })


    $("#btn2").click(function () {
        $("#nonConsegnato").slideToggle();

        $("#lista1").empty();
        $.ajax({
            method: "GET",
            url: "inConsegna",
            success: function (sp) {
                console.log(sp);
               

                    var list1 = `${sp}`
                    $("#lista1").append(list1)

               

            }
        })

    })



    $("#btn3").click(function () {
        $("#citta").slideToggle();

        $("#lista2").empty();
        $.ajax({
            method: "GET",
            url: "Destinazione",
            success: function (sp) {
                console.log(sp);

                $.each(sp, function (i,c) {

                    var list2 = `<li> Città: ${c.Destinazione}
                    <li>${c.Tot}</li>
                    </li>`
                $("#lista2").append(list2)
                } )



            }
        })

    })
})