function handleRadioClick1($radioButton) {
    if ($radioButton.value == 2) {
        $("#hospital").removeAttr("hidden");
        $("#donationCenter").attr("hidden", "hidden");
    } else if ($radioButton.value == 3) {
        $("#donationCenter").removeAttr("hidden");
        $("#hospital").attr("hidden", "hidden");
    } else {
        $("#hospital").attr("hidden", "hidden");
        $("#donationCenter").attr("hidden", "hidden");
    }
}