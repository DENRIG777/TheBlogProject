let index = 0;

function AddTag()
{
    //Get a reference to the TagEntry input element
    var tagEntry = document.getElementById("TagEntry");

    //lets use the new search function to help detect an error state
    let searchResult = search(tagEntry.value);
    if (searchResult != null) {
        //Trigger my sweet alert for what ever condition is contained in the searchResult var
        swalWithDarkButton.fire({
            html: `<span class="font-weight-bolder">${searchResult.toUpperCase()}</span>`
        });
        return true;
    }
    else {
        //Create a new select Option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    //clear out the tagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;

    let tagList = document.getElementById("TagList");
    if (!tagList) return false;

    if (tagList.selectedIndex == -1) {
        swalWithDarkButton.fire({
            html: '<span class="font-weight-bolder">CHOOSE A TAG BEFORE DELETING</span>'
        })

        return true;
    }

    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        } else {
            tagCount = 0;
        }
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected")
})

//look for the tagvalues variable to see if it has data
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        // load up or replaced the options that we have
        ReplaceTag(tagArray[loop], loop);
        index++;
    }
}

function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}

//the search function will detect either an empty or a duplicate Tag
//and return an error is detected
function search(str) {
    if (str == "") {
        return 'Empty tags are not permitted';
    }

    var tagsEl = document.getElementById('TagList');
    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str)
                return 'The Tag #${str} was detected as a duplicate and not permitted'
        }
    }
}

const swalWithDarkButton = swal.mixin({
    customClass: {
        confirmButton: 'btn btn-danger btn-sm btn-block btn-outline-dark'
    },
    imageUrl: '/img/oops.jpg',
    timer: 3000,
    buttonsStyling: false



});