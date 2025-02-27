function localizeText() {
    var culture = $("#cultureName").val();
    if (culture != 'en') {
        var $localizedTextElements = $('.localizedText');
        $localizedTextElements.each(function () {
            var $this = $(this);
            var originalText = $this.text().trim();
            try {
                var localizedText = resourceArray[culture][originalText.trim()];
                if (localizedText !== undefined) {
                    $this.text(localizedText);
                }
            } catch (error) {
                console.error('An error occurred while updating language:', error);
            }
        });
    }
}
$(document).ready(function () {
    localizeText();
});