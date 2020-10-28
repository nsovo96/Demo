var count = 0;
var NextStep = false;
var PrevStep = false;
var Move = 0;
function OzoneAndMunTankLevelEv(Move) {

    $(document).ready(function () {

        
        var chart;
        $.getJSON("/CheckList/TaskAvarage", function (data) {
            // Data gathered from http://populationpyramid.net/germany/2015/

            // Age categories
            if (Move == 0) {
                Move = 1;
                NextStep = true;
                var Cat = data;

                var categories = [];
                var completedAv = [];
                var notCompletedAv = [];
                var total = [];

                count = data.length;


                for (var i = 0; i < data.length; i++) {

                    categories.push(data[i].EmployeeName)
                    completedAv.push(data[i].CompletedTaskEv)
                    notCompletedAv.push(-data[i].NotCompletedTaskEv)

                }

                Highcharts.setOptions({
                    colors: ['#8B0000', '#32CD32']
                });

                Highcharts.chart('container', {


                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: 'Oasis ' + 2020
                    },
                    subtitle: {
                        text: 'total avarage of each employee task status</a>'
                    },
                    accessibility: {
                        point: {
                            valueDescriptionFormat: '{index}. oasisTask {xDescription}, {value}%.'
                        }
                    },
                    xAxis: [{
                        categories: categories,
                        reversed: false,
                        labels: {
                            step: 1
                        },
                        accessibility: {
                            description: 'oasisTask status (Not completed)'
                        }
                    }, { // mirror axis on right side
                        opposite: true,
                        reversed: false,
                        categories: categories,
                        linkedTo: 0,
                        labels: {
                            step: 1
                        },
                        accessibility: {
                            description: 'oasisTask status (Completed)'
                        }
                    }],
                    yAxis: {
                        title: {
                            text: null
                        },
                        labels: {
                            formatter: function () {
                                return Math.abs(this.value)  + '%';
                            }
                        },
                        accessibility: {
                            description: 'total completed vs not completed task %',
                            rangeDescription: 'Range: 0 to 100%'
                        }
                    },

                    plotOptions: {
                        series: {
                            stacking: 'normal'
                        }
                    },

                    tooltip: {
                        formatter: function () {
                            return '<b>' + this.series.name + ', age ' + this.point.category + '</b><br/>' +
                                'oasisTask: ' + Highcharts.numberFormat(Math.abs(this.point.y), 1) + '%';
                        }
                    },
                    series: [{
                        name: 'Not completed task',
                        data: notCompletedAv

                    }, {
                        name: 'Completed task',
                        data: completedAv
                    }]

                });

            } else {
                Peek();
            }
        });

    });
}
function getDateIfDate(d) {
    var m = d.match(/\/Date\((\d+)\)\//);
    return m ? (new Date(+m[1])).toLocaleDateString('en-US', { month: '2-digit', day: '2-digit', year: 'numeric' }) : d;
}
function Peek(Move) {
    Highcharts.setOptions({
        colors: ['#32CD32', '#8B0000']
    });

    $.getJSON("/CheckList/PeekData", function (data) {
        // Data gathered from http://populationpyramid.net/germany/2015/

        // Age categories
        if (Move == 1) {
            Move = 2;
            PrevStep = true;
            var Cat = data;

            var dates = [];
            var completedAv = [];
            var notCompletedAv = [];

            count = data.length;


            for (var i = 0; i < data.length; i++) {

                dates.push(getDateIfDate(data[i].PeekDate))
                completedAv.push(data[i].CompletedTaskEv)
                notCompletedAv.push(data[i].NotCompletedTaskEv)

            }

            Highcharts.chart('container', {
                chart: {
                    type: 'area'
                },
                accessibility: {
                    description: 'Overall task status'
                },
                title: {
                    text: 'Overal completed and not completed task'
                },
                subtitle: {
                    text: 'Sources: <a href="https://thebulletin.org/2006/july/global-nuclear-stockpiles-1945-2006">' +
                        'thebulletin.org</a> &amp; <a href="https://www.armscontrol.org/factsheets/Nuclearweaponswhohaswhat">' +
                        'armscontrol.org</a>'
                },
                xAxis: {
                    allowDecimals: false,
                    type: 'datetime',
                    categories: dates
                },
                yAxis: {
                    title: {
                        text: 'Total task average %'
                    },
                    labels: {
                        formatter: function () {
                            return this.value / 1 + '%';
                        }
                    }
                },
                tooltip: {
                    pointFormat: '{series.name} :  <b>{point.y:,.0f}' + '%'
                },
                plotOptions: {
                    area: {
                        pointStart: 0,
                        marker: {
                            enabled: false,
                            symbol: 'circle',
                            radius: 2,
                            states: {
                                hover: {
                                    enabled: true
                                }
                            }
                        }
                    }
                },
                series: [{
                    name: 'Completed',
                    data: completedAv
                }, {
                    name: 'Not completed',
                    data: notCompletedAv
                }]
            });
        } else {
            FunctionalStatus();
        }
    });


    

}

$(document).ready(function () {


 
    Highcharts.setOptions({
        colors: ['#FF1493', '#800080']
    });
    var chart;
    $.getJSON("/CheckList/getData", function (data) {







        chart = new Highcharts.Chart({

            chart: {

                renderTo: 'container',

                plotBackgroundColor: null,

                plotBorderWidth: null,

                plotShadow: false

            },

            title: {

                text: 'Crystal color effect over time'

            },

            tooltip: {

                formatter: function () {

                    return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';

                }

            },
            credits: {
                enabled: false
            },
            plotOptions: {

                pie: {

                    allowPointSelect: true,

                    cursor: 'pointer',

                    dataLabels: {

                        enabled: true,

                        color: '#000000',

                        connectorColor: '#000000',

                        formatter: function () {

                            return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';

                        }

                    }

                }

            },

            series: [{

                type: 'pie',

                name: 'Browser share',

                data: [

                    ['Pink', data.pink],


                    {

                        name: 'Purple',

                        y: data.purple,

                        sliced: true,

                        selected: true

                    },

                ]

            }]

        });






    });

});


function FunctionalStatus(Move) {


   

    $.getJSON("/CheckList/getData", function (data) {

        if (Move==2) {

            Move = 3;
            Highcharts.chart('container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },
                title: {
                    text: 'Browser<br>shares<br>2017',
                    align: 'center',
                    verticalAlign: 'middle',
                    y: 60
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                accessibility: {
                    point: {
                        valueSuffix: '%'
                    }
                },
                plotOptions: {
                    pie: {
                        dataLabels: {
                            enabled: true,
                            distance: -50,
                            style: {
                                fontWeight: 'bold',
                                color: 'white'
                            }
                        },
                        startAngle: -90,
                        endAngle: 90,
                        center: ['50%', '75%'],
                        size: '110%'
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Ozone status',
                    innerSize: '50%',
                    data: [
                        ['Ozone Ok ', data.TotalOKs],
                        ['Ozone not ok', data.TotalNotOks],

                    ]
                }]
            });


        } else {
            Level()
        }



       

    });

}



function Level(Move) {
    Highcharts.setOptions({
        colors: ['#32CD32', '#8B0000']
    });




    $.getJSON("/CheckList/TanklevesData", function (data) {
        // Data gathered from http://populationpyramid.net/germany/2015/
        // Age categories



        if (Move == 3) {
            Move = 4;
            PrevStep = true;
            var Cat = data;

            var dates = [];
            var completedAv = [];
            var notCompletedAv = [];

            count = data.length;


            for (var i = 0; i < data.length; i++) {

                dates.push(getDateIfDate(data[i].RatioDate))
                completedAv.push(data[i].CleanTanPercentage)
                notCompletedAv.push(data[i].munTankPercentage)

            }



            Highcharts.chart('container', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'daily clean tank and mun tank level'
                },
                xAxis: {
                    categories: dates,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'level (&)'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },


                series: [{
                    name: 'clean tank level',
                    data: completedAv

                }, {
                    name: 'Mun tank level',
                    data: notCompletedAv

                }]
            });



            //Highcharts.chart('container', {
            //    chart: {
            //        type: 'area'
            //    },
            //    accessibility: {
            //        description: 'Overall task status'
            //    },
            //    title: {
            //        text: 'Overal completed and not completed task'
            //    },
            //    subtitle: {
            //        text: 'Sources: <a href="https://thebulletin.org/2006/july/global-nuclear-stockpiles-1945-2006">' +
            //            'thebulletin.org</a> &amp; <a href="https://www.armscontrol.org/factsheets/Nuclearweaponswhohaswhat">' +
            //            'armscontrol.org</a>'
            //    },
            //    xAxis: {
            //        allowDecimals: false,
            //        type: 'datetime',
            //        categories: dates
            //    },
            //    yAxis: {
            //        title: {
            //            text: 'Total task average %'
            //        },
            //        labels: {
            //            formatter: function () {
            //                return this.value / 1 + '%';
            //            }
            //        }
            //    },
            //    tooltip: {
            //        pointFormat: '{series.name} :  <b>{point.y:,.0f}' + '%'
            //    },
            //    plotOptions: {
            //        area: {
            //            pointStart: 0,
            //            marker: {
            //                enabled: false,
            //                symbol: 'circle',
            //                radius: 2,
            //                states: {
            //                    hover: {
            //                        enabled: true
            //                    }
            //                }
            //            }
            //        }
            //    },
            //    series: [{
            //        name: 'Completed',
            //        data: completedAv
            //    }, {
            //        name: 'Not completed',
            //        data: notCompletedAv
            //    }]
            //});
















        } else {
            VolatileOftotalLevel();
        }
    });

}

function VolatileOftotalLevel(Move) {



    $.getJSON("/CheckList/TanklevesData", function (data) {
        // Data gathered from http://populationpyramid.net/germany/2015/
        // Age categories



        if (Move == 4) {
            Move = 5;
            PrevStep = true;
            var Cat = data;

            var dates = [];
            var completedAv = [];
            var notCompletedAv = [];

            count = data.length;
            var total = [];
            var buble = [];



            for (var i = 0; i < data.length; i++) {

                dates.push(getDateIfDate(data[i].RatioDate));
                completedAv.push(data[i].CleanTanPercentage);
                notCompletedAv.push(data[i].munTankPercentage);
                total.push(data[i].TotalLevel)
            }



            //Highcharts.chart('container', {

            //    chart: {
            //        type: 'bubble',
            //        plotBorderWidth: 1,
            //        zoomType: 'xy'
            //    },

            //    legend: {
            //        enabled: false
            //    },

            //    title: {
            //        text: 'Sugar and fat intake per country'
            //    },

            //    subtitle: {
            //        text: 'Source: <a href="http://www.euromonitor.com/">Euromonitor</a> and <a href="https://data.oecd.org/">OECD</a>'
            //    },

            //    accessibility: {
            //        point: {
            //            valueDescriptionFormat: '{index}. {point.name}, fat: {point.x}g, sugar: {point.y}g, obesity: {point.z}%.'
            //        }
            //    },

            //    xAxis: {
            //        gridLineWidth: 1,
            //        title: {
            //            text: 'Daily fat intake'
            //        },
            //        labels: {
            //            format: '{value} gr'
            //        },
            //        plotLines: [{
            //            color: 'black',
            //            dashStyle: 'dot',
            //            width: 2,
            //            value: 65,
            //            label: {
            //                rotation: 0,
            //                y: 15,
            //                style: {
            //                    fontStyle: 'italic'
            //                },
            //                text: 'Safe fat intake 65g/day'
            //            },
            //            zIndex: 3
            //        }],
            //        accessibility: {
            //            rangeDescription: 'Range: 60 to 100 grams.'
            //        }
            //    },

            //    yAxis: {
            //        startOnTick: false,
            //        endOnTick: false,
            //        title: {
            //            text: 'Daily sugar intake'
            //        },
            //        labels: {
            //            format: '{value} gr'
            //        },
            //        maxPadding: 0.2,
            //        plotLines: [{
            //            color: 'black',
            //            dashStyle: 'dot',
            //            width: 2,
            //            value: 50,
            //            label: {
            //                align: 'right',
            //                style: {
            //                    fontStyle: 'italic'
            //                },
            //                text: 'Safe sugar intake 50g/day',
            //                x: -10
            //            },
            //            zIndex: 3
            //        }],
            //        accessibility: {
            //            rangeDescription: 'Range: 0 to 160 grams.'
            //        }
            //    },

            //    tooltip: {
            //        useHTML: true,
            //        headerFormat: '<table>',
            //        pointFormat: '<tr><th colspan="2"><h3>{point.country}</h3></th></tr>' +
            //            '<tr><th>Fat intake:</th><td>{point.x}g</td></tr>' +
            //            '<tr><th>Sugar intake:</th><td>{point.y}g</td></tr>' +
            //            '<tr><th>Obesity (adults):</th><td>{point.z}%</td></tr>',
            //        footerFormat: '</table>',
            //        followPointer: true
            //    },

            //    plotOptions: {
            //        series: {
            //            dataLabels: {
            //                enabled: true,
            //                format: '{point.name}'
            //            }
            //        }
            //    },

            //    series: [{
            //        data: notCompletedAv, completedAv, total

            //    }]

            //});






            Highcharts.chart('container', {

                chart: {
                    type: 'bubble',
                    plotBorderWidth: 1,
                    zoomType: 'xy'
                },

                title: {
                    text: 'Highcharts bubbles with radial gradient fill'
                },

                xAxis: {
                    gridLineWidth: 1,
                    accessibility: {
                        rangeDescription: 'Range: 0 to 100.'
                    }
                },

                yAxis: {
                    startOnTick: false,
                    endOnTick: false,
                    accessibility: {
                        rangeDescription: 'Range: 0 to 100.'
                    }
                },

                series: [{
                    x: completedAv,
                    y: notCompletedAv,
                    data: notCompletedAv

                    ,

                    marker: {
                        fillColor: {
                            radialGradient: { cx: 0.4, cy: 0.3, r: 0.7 },
                            stops: [
                                [0, 'rgba(255,255,255,0.5)'],
                                [1, Highcharts.color(Highcharts.getOptions().colors[0]).setOpacity(0.5).get('rgba')]
                            ]
                        }
                    }

                }]

            });

        } else {
            PFlevel();
        }

    });
}


function PFlevel(Move) {

    

    Highcharts.setOptions({
        colors: ['#8B0000', '#32CD32', '#0341fc','#ebfc03']
    });


    $.getJSON("/CheckList/PfLevels", function (data) {
        // Data gathered from http://populationpyramid.net/germany/2015/
        // Age categories

        if (Move == 5) {
            Move = 6;
            var dates = [];
            var Micron5 = [];
            var micron10 = [];

            count = data.length;
            var Softer = [];
            var sandFilter = [];

            for (var i = 0; i < data.length; i++) {

                dates.push(getDateIfDate(data[i].dateRatio));
                micron10.push(data[i].Pf_Micron_10);
                Softer.push(data[i].Softner);
                sandFilter.push(data[i].SandFiler)
                Micron5.push(data[i].Pf_Micron_5);
            }


            Highcharts.chart('container', {

                title: {
                    text: 'Pf daily data'
                },



                yAxis: {
                    title: {
                        text: 'Pf Scale/50'
                    }
                },

                xAxis: {
                    allowDecimals: false,
                    type: 'datetime',
                    categories: dates
                },

                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle'
                },

                plotOptions: {
                    series: {
                        label: {
                            connectorAllowed: false
                        },
                    }
                },

                series: [{
                    name: 'Sand Filter',
                    data: sandFilter
                }, {
                    name: 'Softner',
                    data: Softer
                }, {
                    name: 'Micron 5',
                    data: Micron5
                }, {
                    name: 'Micron 10',
                    data: micron10
                }],

                responsive: {
                    rules: [{
                        condition: {
                            maxWidth: 500
                        },
                        chartOptions: {
                            legend: {
                                layout: 'horizontal',
                                align: 'center',
                                verticalAlign: 'bottom'
                            }
                        }
                    }]
                }

            });

        } else {
            PressureLevel();
        }

    });

  
}


function PressureLevel(Move) {



    Highcharts.setOptions({
        colors: ['#8B0000', '#32CD32', '#0341fc', '#ebfc03']
    });


    $.getJSON("/CheckList/Pressurelevel", function (data) {
        // Data gathered from http://populationpyramid.net/germany/2015/
        // Age categories

        if (Move == 6) {
            Move = 7;
            var dates = [];
            var memberane = [];
            var microne = [];

            count = data.length;
            var Softer = [];
            var sandFilter = [];

            for (var i = 0; i < data.length; i++) {

                dates.push(getDateIfDate(data[i].dateratio));
                memberane.push(data[i].mebranePressure);
                microne.push(data[i].MicronPressure);

            }


            Highcharts.chart('container', {

                title: {
                    text: 'Pressure daily data'
                },



                yAxis: {
                    title: {
                        text: 'Pf Scale/50'
                    }
                },

                xAxis: {
                    allowDecimals: false,
                    type: 'datetime',
                    categories: dates
                },

                legend: {
                    layout: 'vertical',
                    align: 'right',
                    verticalAlign: 'middle'
                },

                plotOptions: {
                    series: {
                        label: {
                            connectorAllowed: false
                        },
                    }
                },

                series: [{
                    name: 'Membrane Pressure',
                    data: memberane
                }, {
                    name: 'Microne pressure',
                    data: microne
                }],

                responsive: {
                    rules: [{
                        condition: {
                            maxWidth: 500
                        },
                        chartOptions: {
                            legend: {
                                layout: 'horizontal',
                                align: 'center',
                                verticalAlign: 'bottom'
                            }
                        }
                    }]
                }

            });

        } else {
            PupWater()
        }

    });


}


function PupWater(Move) {

    Highcharts.setOptions({
        colors: ['#4e03fc', '#03fcf0']
    });


    $.getJSON("/CheckList/Pump", function (data) {
        // Age categories
 


    if (Move == 7) {
        Move = 8;
        var dates = [];
        var cleanWater = [];
        var RawWater = [];    

        for (var i = 0; i < data.length; i++) {

            dates.push(getDateIfDate(data[i].dateRatio));
            cleanWater.push(data[i].CleanPumed);
            RawWater.push(data[i].RawWater);

        }


        Highcharts.chart('container', {
            chart: {
                type: 'area'
            },
            title: {
                text: 'Area graph that shows loss volume of clean water over raw water *'
            },
            subtitle: {
                align: 'right',
                verticalAlign: 'bottom'
            },
            legend: {
                layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                x: 100,
                y: 70,
                floating: true,
                borderWidth: 1,
                backgroundColor:
                    Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF'
            },
            xAxis: {
                allowDecimals: false,
                type: 'datetime',
                categories: dates
            },
            yAxis: {
                title: {
                    text: 'Y-Axis'
                }
            },
            plotOptions: {
                area: {
                    fillOpacity: 0.5
                }
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'raw water',
                data: RawWater
            }, {
                    name: 'clean water',
                    data: cleanWater
            }]
        });



    }
    });
   
}

const BtnAdd = document.querySelector(".BtnAdd");
const divContainer = document.getElementById("surveycontent");

BtnAdd.addEventListener("click", function () {
    const newForm = document.createElement("div");
    newForm.classList.add("surveyform");
    newForm.appendChild("yeah");
    divContainer.appendChild(newForm);

   // newForm.type = "text";
})


function createlabel(inputname) {
    var label = document.createElement("label");

    label.textContent = inputname;
    return label;

}

function createTask(inputvslue) {
    var task = document.createElement("input");

    task.type="radio"
    task.name = "inputask";
    task.value = inputvslue;
    return task;

}
function createtd(tdname) {
    var tdname = document.createElement("td");

    label.textContent = inputname;
    return tdname;

}

function appendChildreb(parent, children)
{
    children.forEach(function (child) {
        parent.appendChild(child)
    });

}

var move = 0;
function showTaskFront() {

        const Mylist = document.createElement("label");
        var listDiv = document.getElementById("radioList");
        var lbl = document.getElementById("LabelList");
        var bre = document.createElement("br");
        var emprole = document.getElementById("emplrole");


        Mylist.textContent = "Please select a task you want to assign to an employee";
    emprole.value = "FrontEndEmployee";


        var item = [createTask("CELAN FLOORS,WALLS AND COUNTER"), createTask("SANITIZE TAPS AND CLEAN GRID"), createTask("CLEAN FRIDGE"), createTask("CHECK PEST AND SPRAY")];
        var label = [createlabel("CELAN FLOORS,WALLS AND COUNTER"), createlabel("SANITIZE TAPS AND CLEAN GRID"), createlabel("CLEAN FRIDGE"), createlabel("CHECK PEST AND SPRAY")];


        appendChildreb(listDiv, item);
        appendChildreb(lbl, label);
    
  


}



function ProccessAreaEmployee() {

    
        const Mylist = document.createElement("label");

        var listDiv = document.getElementById("radioList");
        var lbl = document.getElementById("LabelList");
        var bre = document.createElement("br");

        Mylist.textContent = "Please select a task you want to assign to an employee";
        var emprole = document.getElementById("emplrole");

    emprole.value = "ProccessAreaEmployee";


        var item = [createTask("WASHBASIN IN THE PROCCESS AREA"), createTask("CLEAN FLOORS, WALLS AND CEILING"), createTask("CEAN STORAGE TANKS CLEAN"), createTask("CLEAN QUIPMENT  AND PLACE THEM IN THE CORRECT PLACE")];
        var label = [createlabel("WASHBASIN IN THE PROCCESS AREA"), createlabel("CLEAN FLOORS, WALLS AND CEILING"), createlabel("CEAN STORAGE TANKS CLEAN"), createlabel("CLEAN QUIPMENT  AND PLACE THEM IN THE CORRECT PLACE")];


        appendChildreb(listDiv, item);
        appendChildreb(lbl, label);
    }


function StorageAreaEmployee() {

   
        const Mylist = document.createElement("label");

        var listDiv = document.getElementById("radioList");
        var lbl = document.getElementById("LabelList");
        var bre = document.createElement("br");
        var emprole = document.getElementById("emplrole");

    emprole.value = "StorageAreaEmployee";

        Mylist.textContent = "Please select a task you want to assign to an employee";

        var item = [createTask("FIX LEAKS"), createTask("CLEAN TAPS AND GRID"), createTask("CLEAN WORKING PIPES")];
        var label = [createlabel("FIX LEAKS"), createlabel("CLEAN TAPS AND GRID"), createlabel("CLEAN WORKING PIPES")];


        appendChildreb(listDiv, item);
        appendChildreb(lbl, label);
    }

function validateForm() {
    var x = document.forms["myForm"]["Employeerole"].value;
    if (x == "") {
        alert("Please click a role you want to assign the task for");
        return false;
    }
}

$(document).ready(function () {

    Highcharts.setOptions({
        colors: ['#FF1493', '#800080']
    });
    var chart;
    var privilage = '<%=Session["username"]%>';
    var completed = 0;
    var notccompleted = 0;
    $.getJSON("/Home/myperformance", function (data) {
        for (var i = 0; i < data.length; i++) {


            if (privilage == data.EmployeeName) {
                completed = data[i].CompletedTaskEv;
                notccompleted = data[i].NotCompletedTaskEv;

            }

        }

      
        chart = new Highcharts.Chart({

            chart: {

                renderTo: 'container',

                plotBackgroundColor: null,

                plotBorderWidth: null,

                plotShadow: false

            },

            title: {

                text: 'Crystal color effect over time'

            },

            tooltip: {

                formatter: function () {

                    return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';

                }

            },
            credits: {
                enabled: false
            },
            plotOptions: {

                pie: {

                    allowPointSelect: true,

                    cursor: 'pointer',

                    dataLabels: {

                        enabled: true,

                        color: '#000000',

                        connectorColor: '#000000',

                        formatter: function () {

                            return '<b>' + this.point.name + '</b>: ' + this.percentage + ' %';

                        }

                    }

                }

            },

            series: [{

                type: 'pie',

                name: 'My Perfomance',

                data: [

                    ['Complted tasks', completed],


                    {

                        name: 'Not completed tasks',

                        y: notccompleted,

                        sliced: true,

                        selected: true

                    },

                ]

            }]

        });

    });

});



$(document).ready(function () {

 
 var chart;
var taskstatus = [];
var taskdate = [];
var taskdetail = [];
    var userID = '@Session["userId"]';

    var completed = 0;
    var notccompleted = 0;
    var tdCount = document.createElement("td");
    var tdDate = document.createElement("td");
    var tdDetails = document.createElement("td");
    var tdTime = document.createElement("td");


    var itemscou = document.getElementById("Myrow");
    var itemsdate=[];
    var itemsdetail;
    var taskstatus=[]
    var itemstime=[];
    var count = [];
    var con = 0;
    var tdname1 = document.createElement("td");
    var tdname2 = document.createElement("td");
    var tdname3 = document.createElement("td");
    var tdname4 = document.createElement("td");
    var tdname5 = document.createElement("td");

    $.getJSON("/Home/OpenTask", function (data) {
        for (var i = 0; i < data.length; i++) {
            if (userID == data[i].Fk_EmployeeID) {

                taskstatus[i] = data[i].TaskStatus;
                taskdate[i] = data[i].dateAssigned;

                taskdetail[i] = data[i].Taskdetail;
                con += 1;
                count[i] = con;
                items.value = taskdetail[i];
            }

        }
        itemscou.appendChild.appendChildreb(tdname1, count);


      appendChildreb(tdname1, count);
        appendChild.appendChildreb(tdname1, count);
       appendChild.appendChildreb(tdname2, taskdate);
      appendChild.appendChildreb(tdname3, taskdetail);
      appendChild.appendChildreb(tdname4, item);



    });

});