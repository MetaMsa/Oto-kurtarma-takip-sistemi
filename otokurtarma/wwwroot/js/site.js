const cursor = document.querySelector(".cursor");

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

    const collection = document.getElementsByTagName("input");
    for (let i = 0; i < collection.length; i++) {
      collection[i].className = "form-control bg-dark text-white";
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

    const collection = document.getElementsByTagName("input");
    for (let i = 0; i < collection.length; i++) {
      collection[i].className = "form-control";
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

        const collection = document.getElementsByTagName("input");
        for (let i = 0; i < collection.length; i++) {
          collection[i].className = "form-control bg-dark text-white";
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

        const collection = document.getElementsByTagName("input");
        for (let i = 0; i < collection.length; i++) {
          collection[i].className = "form-control";
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
