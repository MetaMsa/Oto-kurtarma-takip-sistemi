﻿const cursor = document.querySelector(".cursor");

document.addEventListener("mousemove", function (e) {
  cursor.style.left = e.pageX + "px";
  cursor.style.top = e.pageY + "px";
});

document.addEventListener("mousedown", () => {
  if (getCookie(".Site.isDark") == "no") {
    cursor.style.backgroundColor = "black";
  } else {
    cursor.style.backgroundColor = "yellow";
  }
});

document.addEventListener("mouseup", () => {
  cursor.style.backgroundColor = "";
});

function getCookie(name) {
  const cookies = document.cookie.split(";");
  for (let i = 0; i < cookies.length; i++) {
    const c = cookies[i].trim();
    if (c.startsWith(name + "=")) {
      return c.substring(name.length + 1);
    }
  }
  return null;
}

window.onload = () => {
  if (getCookie(".Site.isDark") == "yes") {
    document.getElementById("themeicon").className = "fa-regular fa-sun fa-2xl";
    document.getElementById("dark-light").className =
      "btn btn-primary bg-gradient";

    const collection = document.querySelectorAll("input, select");
    for (let i = 0; i < collection.length; i++) {
      if (collection[i].tagName == "INPUT")
        collection[i].className = "form-control bg-dark text-white text-center";
      else
        collection[i].className = "form-select bg-dark text-white text-center";
    }

    document.getElementById("nav").className =
      "navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-dark bg-gradient border-bottom box-shadow mb-3";
    document.getElementById("body").className = "bg-secondary bg-gradient";
    document.getElementById("footer").className =
      "container-fluid d-flex justify-content-center flex-wrap border-top bg-dark bg-gradient text-light";
  } else {
    document.getElementById("themeicon").className = "fa-solid fa-moon fa-2xl";
    document.getElementById("dark-light").className =
      "btn btn-dark bg-gradient";

    const collection = document.querySelectorAll("input, select");
    for (let i = 0; i < collection.length; i++) {
      if (collection[i].tagName == "INPUT")
        collection[i].className = "form-control text-center";
      else collection[i].className = "form-select text-center";
    }

    document.getElementById("nav").className =
      "navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-primary bg-gradient border-bottom box-shadow mb-3";
    document.getElementById("body").className = "bg-warning bg-gradient";
    document.getElementById("footer").className =
      "container-fluid d-flex justify-content-center flex-wrap border-top bg-info bg-gradient text";
  }
  setTimeout(() => {
    document.getElementById("load").style.display = "none";
  }, "250");
};

$("#dark-light").on("click", function () {
  $.ajax({
    type: "POST",
    url: "/Home/Darklight",
    headers: {
      RequestVerificationToken: $(
        'input[name="__RequestVerificationToken"]'
      ).val(),
    },
    success: function () {
      console.log(getCookie(".Site.isDark"));
      if (getCookie(".Site.isDark") == "yes") {
        document.getElementById("themeicon").className =
          "fa-regular fa-sun fa-2xl";
        document.getElementById("dark-light").className =
          "btn btn-primary bg-gradient";

        const collection = document.querySelectorAll("input, select");
        for (let i = 0; i < collection.length; i++) {
          if (collection[i].tagName == "INPUT")
            collection[i].className =
              "form-control bg-dark text-white text-center";
          else
            collection[i].className =
              "form-select bg-dark text-white text-center";
        }

        document.getElementById("nav").className =
          "navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-dark bg-gradient border-bottom box-shadow mb-3";
        document.getElementById("body").className = "bg-secondary bg-gradient";
        document.getElementById("footer").className =
          "container-fluid d-flex justify-content-center flex-wrap border-top bg-dark bg-gradient text-light";
      } else {
        document.getElementById("themeicon").className =
          "fa-solid fa-moon fa-2xl";
        document.getElementById("dark-light").className =
          "btn btn-dark bg-gradient";

        const collection = document.querySelectorAll("input, select");
        for (let i = 0; i < collection.length; i++) {
          if (collection[i].tagName == "INPUT")
            collection[i].className =
              "form-control text-center";
          else
            collection[i].className =
              "form-select text-center";
        }

        document.getElementById("nav").className =
          "navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-primary bg-gradient border-bottom box-shadow mb-3";
        document.getElementById("body").className = "bg-warning bg-gradient";
        document.getElementById("footer").className =
          "container-fluid d-flex justify-content-center flex-wrap border-top bg-info bg-gradient text";
      }
    },
  });
});

$(document).ready(function () {
  $("#search").on("input", function () {
    document.getElementById("searchtable").style.display = "block";
    var value = $(this).val().toLocaleUpperCase("TR");

    if (value == "") {
      document.getElementById("searchtable").style.display = "none";
    } else {
      $("#searchtable div").filter(function () {
        $(this).toggle($(this).text().indexOf(value) > -1);
      });
    }
  });
});

const onClickOutside = (selector, callback) => {
  const element = document.querySelector(selector);
  if (!element) return;

  document.addEventListener("click", (e) => {
    if (!element.contains(e.target)) {
      callback();
    }
  });
};
onClickOutside(
  "#searchh",
  () => (document.getElementById("searchtable").style.display = "none")
);
