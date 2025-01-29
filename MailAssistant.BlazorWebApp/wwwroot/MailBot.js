function copyToClipboard(text) {
    navigator.clipboard.writeText(text).then(function() {
        console.log('Text copied to clipboard');
    }).catch(function(error) {
        console.error('Error copying text: ', error);
    });
}
function sendEmail() {
    if (arguments.length == 1) {
        const recipient = 'Please enter your recipient mail id here.';
        const subject = 'Please enter your Subject here';
        window.location.href = `mailto:${recipient}?subject=${encodeURIComponent(subject)}&body=${encodeURIComponent(arguments[0]) }`;
    }
    else if (arguments.length == 3) {
        window.location.href = `mailto:${encodeURIComponent(arguments[1])}?subject=${encodeURIComponent(arguments[2])}&body=${encodeURIComponent(arguments[0])}`;
    }
}

function scrollToBottom() {
    const messages = document.getElementById('messages');
    messages.scrollTop = messages.scrollHeight;
}

function scrollWindowToBottom() {
    window.scrollTo(0, document.body.scrollHeight);
}

function scrollWindowToTop() {
    window.scrollTo(0, 0);
}

function disableButtons() {
    let allButtons = document.querySelectorAll('button');
    allButtons.forEach(button => {
        button.disabled = true;
    });
}

function enableButtons() {
    let allButtons = document.querySelectorAll('button');
    allButtons.forEach(button => {
        button.disabled = false;
    });
}
function simulateChatbotButtonClick() {
    var button = document.getElementById("contact-tab2");
    button.click();
}
