async function preparePage(){
    /*for (const [key, value] of mySearchParams) {

    }*/
    /*const paramsString = document.URL.split("?")[1];
    const searchParams = new URLSearchParams(paramsString);*/
    var ActuallParams = []
    var Brands = []
    for (const [key, value] of new URLSearchParams(document.URL.split("?")[1])) {
        var param = {};
        param.name = key;
        param.value = value;
        ActuallParams.push(param);
    }
    if(ActuallParams.filter((param) => param.name.includes("marque")).length != 0){
        Brands = ActuallParams.filter((param) => param.name.includes("marque"))
        document.querySelectorAll(`input[type="checkbox"]`).forEach((checkbox) => {checkbox.checked = false;})
        Brands.forEach((param) => {
            try{
                var shit = document.querySelector(`input[name="${param.name}"]`)
                shit.checked = true;
            }catch(err){
                console.log(err);
            }
        });
        var brandsVisible = []
        document.querySelectorAll(`input[type="checkbox"]`).forEach((checkbox) => {if(checkbox.checked){brandsVisible.push({name: checkbox.name, value: checkbox.value})}});
        /*document.querySelectorAll(`.carte`).forEach((carte) =>{
            var hidden = document.createAttribute("hidden");
            carte.parentElement.attributes.setNamedItem(hidden)
        })*/
        brandsVisible.forEach((param) => {
            document.querySelectorAll(`.carte[marque-carte=${param.value}]`).forEach((carte) =>{
                carte.parentElement.attributes.removeNamedItem("hidden");
            })
        })
    }
    else{
        document.querySelectorAll(`input[type="checkbox"]`).forEach((checkbox) => {checkbox.checked = true;})
        setTimeout(editPage, 500);
    }
    if(ActuallParams.filter((param) => param.name.includes("est-vedette")).length != 0){
        CarteVedette = ActuallParams.filter((param) => param.name.includes("est-vedette"));
        var selectMenu = document.querySelector("select#est-vedette");
        for(var i = 0; i < selectMenu.options.length; i++){
            if(CarteVedette[0].value == selectMenu.item(i).value){
                selectMenu.options.selectedIndex = i
            }
        } 
    }
    if(ActuallParams.filter((param) => param.name.includes("mot-cle")).length != 0){
        MotCle = ActuallParams.filter((param) => param.name.includes("mot-cle"));
        document.querySelector("#mot-cle").value = MotCle[0].value;
    }
    
   /*
    if(brands){
        
        for (var brand in brands){
            var checked = document.createAttribute("checked")
            document.querySelector(`input[type="checkbox"][id="marque${brand}"][name="marque${brand}"]`).attributes.setNamedItem(checked)
        }
    }
    URLSearchParams.*/
    document.querySelectorAll(`input[type="checkbox"]`).forEach((checkbox) => {
        checkbox.addEventListener("change", editPage);
    })
}
function editPage(){
    var brandsVisible = [];
    document.querySelectorAll(`input[type="checkbox"]`).forEach((checkbox) => {if(checkbox.checked){brandsVisible.push({name: checkbox.name, value: checkbox.value})}});
    document.querySelectorAll(`.carte`).forEach((carte) =>{
        var hidden = document.createAttribute("hidden");
        carte.parentElement.attributes.setNamedItem(hidden)
    })
    brandsVisible.forEach((param) => {
        document.querySelectorAll(`.carte[marque-carte=${param.value}]`).forEach((carte) =>{
            carte.parentElement.attributes.removeNamedItem("hidden");
        })
    })
}
async function run(){
    setTimeout(preparePage, 500)
}
run();