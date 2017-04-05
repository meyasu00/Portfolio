//Fizzbuzz function (Later called for excercise 3)
function fizzBuzz(num1, num2) {
    for (var i = 1; i <= 100; i++) {
        if (i % num1 == 0 && i % num2 == 0) {
            $("#ex3-output").append(i + " FizzBuzz\n");
        } else if (i % num1 == 0) {
            $("#ex3-output").append(i + " Fizz\n");
        } else if (i % num2 == 0) {
            $("#ex3-output").append(i + " Buzz\n");
        } else {
            $("#ex3-output").append(i + "\n");
        }
    };
};

//Palindrome function (Later called for excercise 4)
function palindrome(str) {
    return str.split("").reverse("").join("");
};

//Excercise 1 submit function
$("#ex_btn1").on("click", function () {
    var exVar1 = $("#ex1-item1").val();
    var exVar2 = $("#ex1-item2").val();
    var exVar3 = $("#ex1-item3").val();
    var exVar4 = $("#ex1-item4").val();
    var exVar5 = $("#ex1-item5").val();

    if (exVar1 == "" || exVar2 == "" || exVar3 == "" || exVar4 == "" || exVar5 == "") {
        $("#ex1-output").html("ERROR! Fill out all fields before submitting");
    } else {
        var least = Math.min(exVar1, exVar2, exVar3, exVar4, exVar5);
        var greatest = Math.max(exVar1, exVar2, exVar3, exVar4, exVar5);
        var sum = (+exVar1 + +exVar2 + +exVar3 + +exVar4 + +exVar5);
        var product = exVar1 * exVar2 * exVar3 * exVar4 * exVar5;
        var mean = Math.round(product / 5);

        $("#ex1-output").html(least + " is the smallest number\n");
        $("#ex1-output").append(greatest + " is the largest number\n");
        $("#ex1-output").append(sum + " is the sum of all numbers\n");
        $("#ex1-output").append(product + " is the product of all numbers\n");
        $("#ex1-output").append(mean + " is the mean\n");
    }
});


//Excercise 2 submit function
$("#ex_btn2").on("click", function () {
    factorial = 1;
    var num = $("#ex2-item1").val();
    if (num == "") {
        $("#ex2-output").html("ERROR! Fill out all fields before submitting");
    } else {
        if (num <= 0) {
            $("#ex2-output").html("The factorial of " + num + " is 0");
        } else {
            for (var i = 1; i <= num; i++) {
                factorial *= i;
            }
            $("#ex2-output").html("The factorial of " + num + " is " + factorial);
        }
    }
});

//Excercise 3 submit function
$("#ex_btn3").on("click", function () {
    var num1 = $("#ex3-item1").val();
    var num2 = $("#ex3-item2").val();
    if (num1 == "" | num2 == "") {
        $("#ex3-output").html("ERROR! Fill out all fields before submitting");
    } else {
        $("#ex3-output").html("Start!\n");
        fizzBuzz(num1, num2);
    }
});

//Excercise 4 submit function
$("#ex_btn4").on("click", function () {
    var uInput = $("#ex4-item1").val();
    if (uInput == "") {
        $("#ex4-output").html("ERROR! Fill out all fields before submitting");
    } else {
        //Makes all characters in the string lowercase, and also removes all spaces between words if any
        var str1 = uInput.toLowerCase().replace(/\s+/g, '');
        //Takes string, and splits it into an array, reverses the order, and then recombines the string into the variable "strCompare"
        var strCompare = str1.split("").reverse("").join("").toLowerCase();
        if (str1 === strCompare) {
            $("#ex4-output").append(uInput + " is a palindrome!\n");
        } else {
            $("#ex4-output").append(uInput + " is NOT a palindrome!\n");
        }
    }
});