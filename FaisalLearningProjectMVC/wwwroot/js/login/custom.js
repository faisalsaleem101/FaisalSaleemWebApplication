﻿var _$_f23e = ["\x23\x54\x69\x6D\x65\x72",
    "\x6F\x6E\x6C\x6F\x61\x64",
    "\x68\x74\x6D\x6C",
    "\x64\x69\x73\x61\x62\x6C\x65\x64",
    "\x72\x65\x6D\x6F\x76\x65\x41\x74\x74\x72",
    "\x52\x65\x73\x65\x6E\x64\x20\x56\x65\x72\x69\x66\x69\x63\x61\x74\x69\x6F\x6E\x20\x43\x6F\x64\x65",
    "\x73\x65\x74\x54\x69\x6D\x65\x6F\x75\x74",
    "\x6F\x74\x70",
    "\x67\x65\x74\x45\x6C\x65\x6D\x65\x6E\x74\x42\x79\x49\x64",
    "\x77\x68\x69\x63\x68",
    "\x6B\x65\x79\x43\x6F\x64\x65",
    "\x6C\x65\x6E\x67\x74\x68",
    "\x76\x61\x6C\x75\x65",
    "\x66\x6F\x63\x75\x73",
    "\x62\x6C\x75\x72",
    "\x2E\x69\x6E\x70\x75\x74\x2D\x6F\x74\x70",
    "",
    "\x6A\x6F\x69\x6E",
    "\x63\x61\x6C\x6C",
    "\x6D\x61\x70",
    "\x23\x70\x61\x73\x73\x77\x6F\x72\x64",
    "\x6D\x64\x69\x2D\x65\x79\x65\x20\x6D\x64\x69\x2D\x65\x79\x65\x2D\x6F\x66\x66",
    "\x74\x6F\x67\x67\x6C\x65\x43\x6C\x61\x73\x73",
    "\x74\x79\x70\x65",
    "\x61\x74\x74\x72",
    "\x70\x61\x73\x73\x77\x6F\x72\x64",
    "\x74\x65\x78\x74",
    "\x63\x6C\x69\x63\x6B",
    "\x2E\x74\x6F\x67\x67\x6C\x65\x2D\x70\x61\x73\x73\x77\x6F\x72\x64",
    "\x70\x61\x72\x74\x69\x63\x6C\x65\x73",
    "\x23\x66\x66\x66\x66\x66\x66",
    "\x63\x69\x72\x63\x6C\x65",
    "\x73\x74\x61\x72",
    "\x23\x30\x30\x30\x30\x30\x30",
    "\x6E\x6F\x6E\x65",
    "\x6F\x75\x74",
    "\x63\x61\x6E\x76\x61\x73",
    "\x62\x75\x62\x62\x6C\x65",
    "\x72\x65\x70\x75\x6C\x73\x65"];
var sec = 1;
var Timer = $(_$_f23e[0]);
window[_$_f23e[1]] = countDown;
function countDown() {
    
    if (sec <= 0) {
        $(_$_f23e[0])[_$_f23e[4]](_$_f23e[3]);
        Timer[_$_f23e[2]](_$_f23e[5])
    }
    sec -= 1;
    window[_$_f23e[6]](countDown, 1)
}

function getInputId(b) {
    return document[_$_f23e[8]](_$_f23e[7] + b)
}

function onKeyUpEvent(b, d) {
    const e = d[_$_f23e[9]] || d[_$_f23e[10]];
    if (getInputId(b)[_$_f23e[12]][_$_f23e[11]] === 1) {
        if (b !== 6) {
            getInputId(b + 1)[_$_f23e[13]]()
        }
        else {
            getInputId(b)[_$_f23e[14]]();
            var f = $(_$_f23e[15]);
            sumValue(f)
        }
    }
    if (e === 8 && b !== 1) {
        getInputId(b - 1)[_$_f23e[13]]()
    }
}

function onFocusEvent(b) {
    for (item = 1;
        item < b;
        item++) {
        const c = getInputId(item);
        if (!c[_$_f23e[12]]) {
            c[_$_f23e[13]]();
            break
        }
    }
}

function sumValue(h) {
    sum = [][_$_f23e[19]][_$_f23e[18]](h, function (j) {
        return j[_$_f23e[12]]
    }
    )[_$_f23e[17]](_$_f23e[16]);
    alert(sum);
    for (var g = 0;
        g < h[_$_f23e[11]];
        g++) {
        h[g][_$_f23e[12]] = _$_f23e[16]
    }
}

$(_$_f23e[28])[_$_f23e[27]](function () {
    var a = $(_$_f23e[20]);
    $(this)[_$_f23e[22]](_$_f23e[21]);
    if (a[_$_f23e[24]](_$_f23e[23]) === _$_f23e[25]) {
        a[_$_f23e[24]](_$_f23e[23], _$_f23e[26])
    }
    else {
        a[_$_f23e[24]](_$_f23e[23], _$_f23e[25])
    }
}

);
particlesJS(_$_f23e[29], {
    "\x70\x61\x72\x74\x69\x63\x6C\x65\x73": {
        "\x6E\x75\x6D\x62\x65\x72": {
            "\x76\x61\x6C\x75\x65": 40, "\x64\x65\x6E\x73\x69\x74\x79": {
                "\x65\x6E\x61\x62\x6C\x65": true, "\x76\x61\x6C\x75\x65\x5F\x61\x72\x65\x61": 500
            }
        }
        , "\x63\x6F\x6C\x6F\x72": {
            "\x76\x61\x6C\x75\x65": _$_f23e[30]
        }
        , "\x73\x68\x61\x70\x65": {
            "\x74\x79\x70\x65": [_$_f23e[31], _$_f23e[32]], "\x73\x74\x72\x6F\x6B\x65": {
                "\x77\x69\x64\x74\x68": 0, "\x63\x6F\x6C\x6F\x72": _$_f23e[33]
            }
            , "\x70\x6F\x6C\x79\x67\x6F\x6E": {
                "\x6E\x62\x5F\x73\x69\x64\x65\x73": 5
            }
        }
        , "\x6F\x70\x61\x63\x69\x74\x79": {
            "\x76\x61\x6C\x75\x65": 1, "\x72\x61\x6E\x64\x6F\x6D": true, "\x61\x6E\x69\x6D": {
                "\x65\x6E\x61\x62\x6C\x65": true, "\x73\x70\x65\x65\x64": 1, "\x6F\x70\x61\x63\x69\x74\x79\x5F\x6D\x69\x6E": 0, "\x73\x79\x6E\x63": false
            }
        }
        , "\x73\x69\x7A\x65": {
            "\x76\x61\x6C\x75\x65": 3, "\x72\x61\x6E\x64\x6F\x6D": true, "\x61\x6E\x69\x6D": {
                "\x65\x6E\x61\x62\x6C\x65": true, "\x73\x70\x65\x65\x64": 3, "\x73\x69\x7A\x65\x5F\x6D\x69\x6E": 0.2, "\x73\x79\x6E\x63": true
            }
        }
        , "\x6C\x69\x6E\x65\x5F\x6C\x69\x6E\x6B\x65\x64": {
            "\x65\x6E\x61\x62\x6C\x65": false, "\x64\x69\x73\x74\x61\x6E\x63\x65": 150, "\x63\x6F\x6C\x6F\x72": _$_f23e[30], "\x6F\x70\x61\x63\x69\x74\x79": 0.4, "\x77\x69\x64\x74\x68": 1
        }
        , "\x6D\x6F\x76\x65": {
            "\x65\x6E\x61\x62\x6C\x65": true, "\x73\x70\x65\x65\x64": 1, "\x64\x69\x72\x65\x63\x74\x69\x6F\x6E": _$_f23e[34], "\x72\x61\x6E\x64\x6F\x6D": true, "\x73\x74\x72\x61\x69\x67\x68\x74": false, "\x6F\x75\x74\x5F\x6D\x6F\x64\x65": _$_f23e[35], "\x62\x6F\x75\x6E\x63\x65": false, "\x61\x74\x74\x72\x61\x63\x74": {
                "\x65\x6E\x61\x62\x6C\x65": false, "\x72\x6F\x74\x61\x74\x65\x58": 600, "\x72\x6F\x74\x61\x74\x65\x59": 600
            }
        }
    }
    , "\x69\x6E\x74\x65\x72\x61\x63\x74\x69\x76\x69\x74\x79": {
        "\x64\x65\x74\x65\x63\x74\x5F\x6F\x6E": _$_f23e[36], "\x65\x76\x65\x6E\x74\x73": {
            "\x6F\x6E\x68\x6F\x76\x65\x72": {
                "\x65\x6E\x61\x62\x6C\x65": false, "\x6D\x6F\x64\x65": _$_f23e[37]
            }
            , "\x6F\x6E\x63\x6C\x69\x63\x6B": {
                "\x65\x6E\x61\x62\x6C\x65": false, "\x6D\x6F\x64\x65": _$_f23e[38]
            }
            , "\x72\x65\x73\x69\x7A\x65": true
        }
        , "\x6D\x6F\x64\x65\x73": {
            "\x67\x72\x61\x62": {
                "\x64\x69\x73\x74\x61\x6E\x63\x65": 400, "\x6C\x69\x6E\x65\x5F\x6C\x69\x6E\x6B\x65\x64": {
                    "\x6F\x70\x61\x63\x69\x74\x79": 1
                }
            }
            , "\x62\x75\x62\x62\x6C\x65": {
                "\x64\x69\x73\x74\x61\x6E\x63\x65": 250, "\x73\x69\x7A\x65": 0, "\x64\x75\x72\x61\x74\x69\x6F\x6E": 2, "\x6F\x70\x61\x63\x69\x74\x79": 0, "\x73\x70\x65\x65\x64": 3
            }
            , "\x72\x65\x70\x75\x6C\x73\x65": {
                "\x64\x69\x73\x74\x61\x6E\x63\x65": 400, "\x64\x75\x72\x61\x74\x69\x6F\x6E": 0.4
            }
            , "\x70\x75\x73\x68": {
                "\x70\x61\x72\x74\x69\x63\x6C\x65\x73\x5F\x6E\x62": 4
            }
            , "\x72\x65\x6D\x6F\x76\x65": {
                "\x70\x61\x72\x74\x69\x63\x6C\x65\x73\x5F\x6E\x62": 2
            }
        }
    }
    , "\x72\x65\x74\x69\x6E\x61\x5F\x64\x65\x74\x65\x63\x74": true
}

)