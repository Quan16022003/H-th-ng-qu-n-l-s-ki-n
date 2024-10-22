function setUnactiveAll() {
	let containers = ["personal", "security"];

	containers.forEach(classname => {
		let container = document.getElementsByClassName(classname);
		container[0].classList.remove("active");

		let form = document.getElementsByClassName(`${classname}-form`)
		form[0].classList.remove("active");
	})
}

function setActive(classname) {
	let container = document.getElementsByClassName(classname);
	let form = document.getElementsByClassName(`${classname}-form`)
	if (container[0].classList.contains("active")) return;

	setUnactiveAll();
	container[0].classList.add("active");
	form[0].classList.add("active");
}

function onTabChange(cur) {
	let view = "";

	if (cur == "") {
		view = window.location.href.toString().toLowerCase().split('/').pop();

		// if not default page, navigate with Index/blah
		// else return /Index url
		cur = view != "index" ? "Index/" + view : "Index";
	}
	else view = cur.toLowerCase().split('/').pop();

	let baseUrl = "/Profile/Profile/";

	switch (view) {
		case "index":
			setActive("personal");
			break;

		case "security":
			setActive("security")
			break;
	}

	window.history.replaceState(
		"",
		"",
		baseUrl + cur);
}

onTabChange("");

window.addEventListener("popstate", (e) => {
	if (e.state) {
		onTabChange("");
	}
});