// store open or close based on user click
let isClose = false;
// store open or close based on user hover
let isOpenHover = false;

//#region event

let groups = Array.from(document.querySelectorAll(".group"));

groups.forEach(aTag => {
    let parent = aTag.parentElement;
    let group = parent.querySelector(".item-child");

    if (parent.classList.contains("selected")) onGroupClick(group, aTag);

    aTag.addEventListener("click", () => {
        onGroupClick(group, aTag);
    })
});

let sideMenu = document.getElementsByClassName("side-menu")[0];
let content = document.getElementsByClassName("body-container")[0];

// open when hover
sideMenu.addEventListener("mouseenter", () => {
    if (isClose) {
        if (!isOpenHover) onSideMenuButtonClick(false);
    }
});

// close when out
content.addEventListener("mouseenter", () => {
    setTimeout(() => {
        if (isClose) {
            if (isOpenHover) onSideMenuButtonClick(false);
        }
    }, 250);
});

let sideMenuButton = document.getElementsByClassName("side-menu-button")[0];
sideMenuButton.addEventListener("click", () => {
    onSideMenuButtonClick(true);
    changeSideMenuButtonIcon(sideMenuButton);
});

function onGroupClick(container, aTag) {
    //select
    if (container.classList.contains("collapse")) {
        container.classList.remove("collapse");
        aTag.classList.add("selected");
    }
    //close
    else {
        container.classList.add("collapse");
        aTag.classList.remove("selected");
    }
}

function closeAllGroup() {
    groups.forEach(aTag => {
        let parent = aTag.parentElement;
        let group = parent.querySelector(".item-child");
        closeGroup(group, aTag);
    });
}

function closeGroup(container, aTag) {
    if (!container.classList.contains("collapse")) {
        container.classList.add("collapse");
        aTag.classList.remove("selected");
    }
}

function onSideMenuButtonClick(isClick) {
    let sideMenu = document.getElementsByClassName("side-menu")[0];
    let box = document.getElementsByClassName("box")[0];

    if (isClick) isClose = !isClose;

    // close
    if (sideMenu.classList.contains("open")) {
        closeAllGroup();

        sideMenu.classList.remove("open");
        sideMenu.classList.add("close");

        box.classList.remove("open");
        box.classList.add("close");

        if (!isClick) isOpenHover = false;
    }
    // open
    else if (sideMenu.classList.contains("close")) {
        sideMenu.classList.remove("close");
        setTimeout(() => sideMenu.classList.add("open"), 250);

        box.classList.remove("close");
        box.classList.add("open");

        if (!isClick) isOpenHover = true;
    }
}

function changeSideMenuButtonIcon(button) {
    let child = button.children;
    let close = child[0];
    let open = child[1];

    if (isClose) {
        close.classList.add("hide-icon");
        open.classList.remove("hide-icon");
    }
    else {
        open.classList.add("hide-icon");
        close.classList.remove("hide-icon");
    }
}

//#endregion