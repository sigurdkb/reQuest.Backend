(function(){

    var values = []; var labels = []
    values[0] = 5; //@(some_numeric_variable_from_viewmodel)
    values[1] = 3; //@(some_numeric_variable_from_viewmodel)
    values[2] = 7; //@(some_numeric_variable_from_viewmodel)

    labels[0] = "Aktive"; //'@(some_string_variable_from_viewmodel)'
    labels[1] = "Utgått på tid"; //'@(some_string_variable_from_viewmodel)'
    labels[2] = "Noe annet?"; //'@(some_string_variable_from_viewmodel)'

    var opt =  {
        legend:{
            onClick:function(evt, item){
                //todo - link til side
            }
        },
        onClick: function(evt, item){
            
            var piepieceIndex = item[0]._index;
            //todo - link til side
        }
    }

    var data = {
        labels,
        datasets: [
            {
                data: values,
                backgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56"
                ]
            }]
    };

    var myChart = new Chart($("#cnv"), {
        type: 'doughnut',
        options: opt,
        data: data
    });   


})();