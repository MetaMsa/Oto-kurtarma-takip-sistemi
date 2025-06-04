function darkLight()
{
    if(document.getElementById("themeicon").className == "fa-solid fa-moon fa-2xl")
    {
        document.getElementById("themeicon").className = "fa-regular fa-sun fa-2xl";
        document.getElementById("dark-light").className = "btn btn-primary";

        document.getElementById("nav").className = "navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-dark border-bottom box-shadow mb-3";
        document.getElementById("body").className = "bg-secondary";
        document.getElementById("footer").className = "container-fluid d-flex justify-content-center flex-wrap border-top bg-dark text-light";
    }
    else{
        document.getElementById("themeicon").className = "fa-solid fa-moon fa-2xl";
        document.getElementById("dark-light").className = "btn btn-dark";

        document.getElementById("nav").className = "navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-primary bg-gradient border-bottom box-shadow mb-3";
        document.getElementById("body").className = "bg-warning bg-gradient";
        document.getElementById("footer").className = "container-fluid d-flex justify-content-center flex-wrap border-top bg-info bg-gradient text";
    }
}