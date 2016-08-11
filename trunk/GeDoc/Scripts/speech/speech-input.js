/*global webkitSpeechRecognition */
function capitalize(str) {
    return str.length ? str[0].toUpperCase() + str.slice(1) : str;
}

function InicializarMicrofono(Speacker, divContenedor, inpuTexto, btnMicro, EsAutomatico, callFuncion, callFuncionOnStart, callFuncionOnEnd) {
    //var recognition;
    (function() {
        'use strict';

        if (! ('webkitSpeechRecognition' in window)) return;

        var talkMsg = 'Hable ahora!';
        //var patience = 60;

        var speechInputWrappers = document.getElementsByClassName(divContenedor);

        [].forEach.call(speechInputWrappers, function(speechInputWrapper) {
            // find elements
            //var inputEl = speechInputWrapper.querySelector("#" + inpuTexto);
            var micBtn = speechInputWrapper.querySelector("#" + btnMicro);

            // size and position them
            //var inputHeight = inputEl.offsetHeight;
            //var inputRightBorder = parseInt(getComputedStyle(inputEl).borderRightWidth, 10);
            //var buttonSize = 0.8 * inputHeight;
            //inputEl.value = '';
            //micBtn.style.top = 0.1 * inputHeight + 'px';
            //micBtn.style.height = micBtn.style.width = buttonSize + 'px';
            //inputEl.style.paddingRight = buttonSize - inputRightBorder + 'px';
            if (!EsAutomatico) {
                speechInputWrapper.appendChild(micBtn);
            }

            // setup recognition
            var finalTranscript = '';
            var recognizing = false;
            var timeout;
            var oldPlaceholder = null;
            Speacker.recognition = new webkitSpeechRecognition();
            Speacker.recognition.continuous = true;

            function restartTimer() {
                timeout = setTimeout(function() {
                    Speacker.recognition.stop();
                }, 60 * 1000);
            }

            Speacker.recognition.onstart = function () {
                //oldPlaceholder = inputEl.placeholder;
                //inputEl.placeholder = talkMsg;
                recognizing = true;
                //finalTranscript = inputEl.value;
                if (!EsAutomatico) {
                    micBtn.classList.add('listening');
                }
                if (callFuncionOnStart != null) {
                    callFuncionOnStart();
                }
                restartTimer();
            };

            Speacker.recognition.onend = function () {
                recognizing = false;
                clearTimeout(timeout);
                if (!EsAutomatico) {
                    micBtn.classList.remove('listening');
                }
                //if (oldPlaceholder !== null) inputEl.placeholder = oldPlaceholder;
                if (callFuncionOnEnd != null) {
                    callFuncionOnEnd();
                }
            };

            Speacker.recognition.onresult = function (event) {
                //if (!EsAutomatico) {
                    clearTimeout(timeout);
                //}
                for (var i = event.resultIndex; i < event.results.length; ++i) {
                    if (event.results[i].isFinal) {
                        finalTranscript += event.results[i][0].transcript;
                    }
                }
                finalTranscript = capitalize(finalTranscript);
                //inputEl.value = finalTranscript;
                if (callFuncion != null) {
                    callFuncion(finalTranscript);
                    finalTranscript = "";
                }
                //if (!EsAutomatico) {
                    restartTimer();
                //}
            };

            if (!EsAutomatico) {
                micBtn.addEventListener('click', function (event) {
                    event.preventDefault();
                    if (recognizing) {
                        Speacker.recognition.stop();
                        return;
                    }
                    finalTranscript = "";
                    Speacker.recognition.start();
                }, false);
            } else {
                if (recognizing) {
                    Speacker.recognition.stop();
                    return;
                }
                finalTranscript = "";
                Speacker.recognition.start();
            }
        });
    })();

    if (EsAutomatico) {
        $("#" + btnMicro).trigger("click");
    }

    return
}