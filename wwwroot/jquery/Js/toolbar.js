function clickSentNotification(className: string, id: string, moduleId: string): void {
    $(`.${className}`).on("click", () => {
        $.ajax({
            type: "POST",
            url: "/api/notification/SentNotification",
            data: { id, moduleId },
            dataType: "json",
            success: () => { },
            error: (xhr) => {
                console.error("Error:", xhr?.responseJSON?.message || xhr.statusText);
            }
        });
    });
}

function clickNavigation(className: string): void {
    $(`.${className}`).on("click", () => {
        const div = document.getElementById("div-navbar") as HTMLDivElement | null;
        const img = document.getElementById("img-navbar") as HTMLImageElement | null;
        const divMain = document.getElementById("div-mainbar") as HTMLDivElement | null;

        if (div && img && divMain) {
            const isVisible = div.style.display !== "none";
            div.style.display = isVisible ? "none" : "block";
            img.src = isVisible ? "/icon/navbar-show.png" : "/icon/navbar-hide.png";
            SetNavigationList(isVisible ? "true" : "false");

            divMain.classList.toggle("col-md-10", !isVisible);
            divMain.classList.toggle("col-md-12", isVisible);
        }
    });
}

function toggleVisibility(className: string, divId: string, imgId: string, showIcon: string, hideIcon: string): void {
    $(`.${className}`).on("click", () => {
        const div = document.getElementById(divId) as HTMLDivElement | null;
        const img = document.getElementById(imgId) as HTMLImageElement | null;

        if (div && img) {
            const isVisible = div.style.display !== "none";
            div.style.display = isVisible ? "none" : "block";
            img.src = isVisible ? showIcon : hideIcon;
        }
    });
}

function showError(className: string): void {
    toggleVisibility(className, "div-error", "img-error", "/icon/div-error-show.png", "/icon/div-error-hide.png");
}

function showSuccess(className: string): void {
    toggleVisibility(className, "div-sucess", "img-sucess", "/icon/div-sucess-show.png", "/icon/div-sucess-hide.png");
}

function SetClassDivMain(): void {
    const div = document.getElementById("div-navbar") as HTMLDivElement | null;
    const divMain = document.getElementById("div-mainbar") as HTMLDivElement | null;

    if (div && divMain) {
        const isHidden = div.style.display === "none";
        divMain.classList.toggle("col-md-10", !isHidden);
        divMain.classList.toggle("col-md-12", isHidden);
    }
}

function SetNavigationList(hide: string): void {
    $.ajax({
        type: "POST",
        url: "/api/tools/SetNavigationList",
        data: { hide },
        dataType: "json",
        success: () => { },
        error: (xhr) => {
            console.error("Error:", xhr?.responseJSON?.message || xhr.statusText);
        }
    });
}

function clickButtonForm(className: string, actionUrl: string): void {
    $(`.${className}`).on("click", () => {
        $("form").attr("action", actionUrl).submit();
    });
}

function BoolCheckBoxTable(className: string, moduleId: string): void {
    $(`.${className}`).on("change", (e) => {
        const value = (e.currentTarget as HTMLInputElement).value;

        $.ajax({
            type: "POST",
            url: "/api/tools/GetBoolDb",
            data: { moduleId, value },
            dataType: "json",
            success: () => { },
            error: () => {
                console.error("Error while fetching boolean value");
            }
        });
    });
}

function CheckBoolCheckBox(className: string, targetClass: string): void {
    $(`.${className}`).on("change", function () {
        const isChecked = $(this).prop("checked");
        const target = $(this).closest("tr").find(`.${targetClass}`);
        target.css("display", isChecked ? "" : "none");
    });
}

function CheckBoolItemSelect(className: string, targetClass: string): void {
    $(`.${className}`).on("change", function () {
        const value = parseInt((this as HTMLSelectElement).value, 10);
        const target = $(this).closest("tr").find(`.${targetClass}`);
        target.css("display", value === 1 ? "" : "none");
    });
}

function CheckBoolCheckBoxDisplayInput(className: string, targetClass: string): void {
    $(`.${className}`).on("change", function () {
        const isChecked = $(this).prop("checked");
        $(`.${targetClass}`).css("display", isChecked ? "" : "none");
    });
}

function SetSameDate(className: string, targetId: string): void {
    $(`.${className}`).on("change", function () {
        const value = $(this).val();

        $.ajax({
            type: "POST",
            url: "/api/tools/GetSameDay",
            data: { datum: value },
            dataType: "json",
            success: (result) => {
                if (result?.data) {
                    $(`#${targetId}`).val(result.data);
                }
            },
            error: () => {
                console.error("Error while syncing date");
            }
        });
    });
}
