const cursor = document.querySelector(".cursor");

document.addEventListener("mousemove", function (e) {
  cursor.style.left = e.pageX + "px";
  cursor.style.top = e.pageY + "px";
});

document.addEventListener("mousedown", () => {
    if (getCookie(".Site.isDark") == "no") {
        cursor.style.backgroundColor = "black";
    }
    else{
        cursor.style.backgroundColor = "yellow";
    }
});

document.addEventListener("mouseup", () => {
  cursor.style.backgroundColor = "";
});
