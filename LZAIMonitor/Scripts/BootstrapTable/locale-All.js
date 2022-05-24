﻿/**
 * bootstrap-table - An extended Bootstrap table with radio, checkbox, sort, pagination, and other added features. (supports twitter bootstrap v2 and v3).
 *
 * @version v1.14.2
 * @homepage https://bootstrap-table.com
 * @author wenzhixin <wenzhixin2010@gmail.com> (http://wenzhixin.net.cn/)
 * @license MIT
 */

(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableAfZA = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['af-ZA'] = {
            formatLoadingMessage: function () {
                return 'Besig om te laai, wag asseblief'
            },
            formatRecordsPerPage: function (a) {
                return a + ' rekords per bladsy'
            },
            formatShowingRows: function (a, b, c) {
                return 'Resultate ' + a + ' tot ' + b + ' van ' + c + ' rye'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Soek'
            },
            formatNoMatches: function () {
                return 'Geen rekords gevind nie'
            },
            formatPaginationSwitch: function () {
                return 'Wys/verberg bladsy nummering'
            },
            formatRefresh: function () {
                return 'Herlaai'
            },
            formatToggle: function () {
                return 'Wissel'
            },
            formatColumns: function () {
                return 'Kolomme'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['af-ZA'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableArSA = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['ar-SA'] = {
            formatLoadingMessage: function () {
                return '\u062C\u0627\u0631\u064A \u0627\u0644\u062A\u062D\u0645\u064A\u0644, \u064A\u0631\u062C\u0649 \u0627\u0644\u0625\u0646\u062A\u0638\u0627\u0631'
            },
            formatRecordsPerPage: function (a) {
                return a + ' \u0633\u062C\u0644 \u0644\u0643\u0644 \u0635\u0641\u062D\u0629'
            },
            formatShowingRows: function (a, b, c) {
                return '\u0627\u0644\u0638\u0627\u0647\u0631 ' + a + ' \u0625\u0644\u0649 ' + b + ' \u0645\u0646 ' + c + ' \u0633\u062C\u0644'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u0628\u062D\u062B'
            },
            formatNoMatches: function () {
                return '\u0644\u0627 \u062A\u0648\u062C\u062F \u0646\u062A\u0627\u0626\u062C \u0645\u0637\u0627\u0628\u0642\u0629 \u0644\u0644\u0628\u062D\u062B'
            },
            formatPaginationSwitch: function () {
                return '\u0625\u062E\u0641\u0627\u0621\u0625\u0638\u0647\u0627\u0631 \u062A\u0631\u0642\u064A\u0645 \u0627\u0644\u0635\u0641\u062D\u0627\u062A'
            },
            formatRefresh: function () {
                return '\u062A\u062D\u062F\u064A\u062B'
            },
            formatToggle: function () {
                return '\u062A\u063A\u064A\u064A\u0631'
            },
            formatColumns: function () {
                return '\u0623\u0639\u0645\u062F\u0629'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['ar-SA'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableCaES = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['ca-ES'] = {
            formatLoadingMessage: function () {
                return 'Espereu, si us plau'
            },
            formatRecordsPerPage: function (a) {
                return a + ' resultats per p\xE0gina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Mostrant de ' + a + ' fins ' + b + ' - total ' + c + ' resultats'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Cerca'
            },
            formatNoMatches: function () {
                return 'No s\'han trobat resultats'
            },
            formatPaginationSwitch: function () {
                return 'Amaga/Mostra paginaci\xF3'
            },
            formatRefresh: function () {
                return 'Refresca'
            },
            formatToggle: function () {
                return 'Alterna formataci\xF3'
            },
            formatColumns: function () {
                return 'Columnes'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Tots'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['ca-ES'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableCsCZ = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['cs-CZ'] = {
            formatLoadingMessage: function () {
                return '\u010Cekejte, pros\xEDm'
            },
            formatRecordsPerPage: function (a) {
                return a + ' polo\u017Eek na str\xE1nku'
            },
            formatShowingRows: function (a, b, c) {
                return 'Zobrazena ' + a + '. - ' + b + '. polo\u017Eka z celkov\xFDch ' + c
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Vyhled\xE1v\xE1n\xED'
            },
            formatNoMatches: function () {
                return 'Nenalezena \u017E\xE1dn\xE1 vyhovuj\xEDc\xED polo\u017Eka'
            },
            formatPaginationSwitch: function () {
                return 'Skr\xFDt/Zobrazit str\xE1nkov\xE1n\xED'
            },
            formatRefresh: function () {
                return 'Aktualizovat'
            },
            formatToggle: function () {
                return 'P\u0159epni'
            },
            formatColumns: function () {
                return 'Sloupce'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'V\u0161e'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['cs-CZ'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableDaDK = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['da-DK'] = {
            formatLoadingMessage: function () {
                return 'Indl\xE6ser, vent venligst'
            },
            formatRecordsPerPage: function (a) {
                return a + ' poster pr side'
            },
            formatShowingRows: function (a, b, c) {
                return 'Viser ' + a + ' til ' + b + ' af ' + c + ' r\xE6kke' + (1 < c ? 'r' : '')
            },
            formatDetailPagination: function (a) {
                return 'Viser ' + a + ' r\xE6kke' + (1 < a ? 'r' : '')
            },
            formatSearch: function () {
                return 'S\xF8g'
            },
            formatNoMatches: function () {
                return 'Ingen poster fundet'
            },
            formatPaginationSwitch: function () {
                return 'Skjul/vis nummerering'
            },
            formatRefresh: function () {
                return 'Opdater'
            },
            formatToggle: function () {
                return 'Skift'
            },
            formatColumns: function () {
                return 'Kolonner'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Alle'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Eksporter'
            },
            formatClearFilters: function () {
                return 'Ryd filtre'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['da-DK'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableDeDE = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['de-DE'] = {
            formatLoadingMessage: function () {
                return 'Lade, bitte warten'
            },
            formatRecordsPerPage: function (a) {
                return a + ' Zeilen pro Seite.'
            },
            formatShowingRows: function (a, b, c) {
                return 'Zeige Zeile ' + a + ' bis ' + b + ' von ' + c + ' Zeile' + (1 < c ? 'n' : '') + '.'
            },
            formatDetailPagination: function (a) {
                return 'Zeige ' + a + ' Zeile' + (1 < a ? 'n' : '') + '.'
            },
            formatSearch: function () {
                return 'Suchen'
            },
            formatNoMatches: function () {
                return 'Keine passenden Ergebnisse gefunden'
            },
            formatPaginationSwitch: function () {
                return 'Verstecke/Zeige Nummerierung'
            },
            formatRefresh: function () {
                return 'Neu laden'
            },
            formatToggle: function () {
                return 'Umschalten'
            },
            formatColumns: function () {
                return 'Spalten'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Alle'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Datenexport'
            },
            formatClearFilters: function () {
                return 'L\xF6sche Filter'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['de-DE'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableElGR = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['el-GR'] = {
            formatLoadingMessage: function () {
                return '\u03A6\u03BF\u03C1\u03C4\u03CE\u03BD\u03B5\u03B9, \u03C0\u03B1\u03C1\u03B1\u03BA\u03B1\u03BB\u03CE \u03C0\u03B5\u03C1\u03B9\u03BC\u03AD\u03BD\u03B5\u03C4\u03B5'
            },
            formatRecordsPerPage: function (a) {
                return a + ' \u03B1\u03C0\u03BF\u03C4\u03B5\u03BB\u03AD\u03C3\u03BC\u03B1\u03C4\u03B1 \u03B1\u03BD\u03AC \u03C3\u03B5\u03BB\u03AF\u03B4\u03B1'
            },
            formatShowingRows: function (a, b, c) {
                return '\u0395\u03BC\u03C6\u03B1\u03BD\u03AF\u03B6\u03BF\u03BD\u03C4\u03B1\u03B9 \u03B1\u03C0\u03CC \u03C4\u03B7\u03BD ' + a + ' \u03C9\u03C2 \u03C4\u03B7\u03BD ' + b + ' \u03B1\u03C0\u03CC \u03C3\u03CD\u03BD\u03BF\u03BB\u03BF ' + c + ' \u03C3\u03B5\u03B9\u03C1\u03CE\u03BD'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u0391\u03BD\u03B1\u03B6\u03B7\u03C4\u03AE\u03C3\u03C4\u03B5'
            },
            formatNoMatches: function () {
                return '\u0394\u03B5\u03BD \u03B2\u03C1\u03AD\u03B8\u03B7\u03BA\u03B1\u03BD \u03B1\u03C0\u03BF\u03C4\u03B5\u03BB\u03AD\u03C3\u03BC\u03B1\u03C4\u03B1'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Refresh'
            },
            formatToggle: function () {
                return 'Toggle'
            },
            formatColumns: function () {
                return 'Columns'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['el-GR'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEnUS = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['en-US'] = {
            formatLoadingMessage: function () {
                return 'Loading, please wait'
            },
            formatRecordsPerPage: function (a) {
                return a + ' rows per page'
            },
            formatShowingRows: function (a, b, c) {
                return 'Showing ' + a + ' to ' + b + ' of ' + c + ' rows'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Search'
            },
            formatNoMatches: function () {
                return 'No matching records found'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Refresh'
            },
            formatToggle: function () {
                return 'Toggle'
            },
            formatColumns: function () {
                return 'Columns'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['en-US'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEsAR = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['es-AR'] = {
            formatLoadingMessage: function () {
                return 'Cargando, espere por favor'
            },
            formatRecordsPerPage: function (a) {
                return a + ' registros por p\xE1gina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Mostrando ' + a + ' a ' + b + ' de ' + c + ' filas'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Buscar'
            },
            formatNoMatches: function () {
                return 'No se encontraron registros'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Refresh'
            },
            formatToggle: function () {
                return 'Toggle'
            },
            formatColumns: function () {
                return 'Columns'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Todo'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['es-AR'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEsCL = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['es-CL'] = {
            formatLoadingMessage: function () {
                return 'Cargando, espere por favor'
            },
            formatRecordsPerPage: function (a) {
                return a + ' filas por p\xE1gina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Mostrando ' + a + ' a ' + b + ' de ' + c + ' filas'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Buscar'
            },
            formatNoMatches: function () {
                return 'No se encontraron registros'
            },
            formatPaginationSwitch: function () {
                return 'Ocultar/Mostrar paginaci\xF3n'
            },
            formatRefresh: function () {
                return 'Refrescar'
            },
            formatToggle: function () {
                return 'Cambiar'
            },
            formatColumns: function () {
                return 'Columnas'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Todo'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['es-CL'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEsCR = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['es-CR'] = {
            formatLoadingMessage: function () {
                return 'Cargando, por favor espere'
            },
            formatRecordsPerPage: function (a) {
                return a + ' registros por p\xE1gina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Mostrando de ' + a + ' a ' + b + ' registros de ' + c + ' registros en total'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Buscar'
            },
            formatNoMatches: function () {
                return 'No se encontraron registros'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Refrescar'
            },
            formatToggle: function () {
                return 'Alternar'
            },
            formatColumns: function () {
                return 'Columnas'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Todo'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['es-CR'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEsES = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['es-ES'] = {
            formatLoadingMessage: function () {
                return 'Por favor espere'
            },
            formatRecordsPerPage: function (a) {
                return a + ' resultados por p\xE1gina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Mostrando desde ' + a + ' hasta ' + b + ' - En total ' + c + ' resultados'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Buscar'
            },
            formatNoMatches: function () {
                return 'No se encontraron resultados'
            },
            formatPaginationSwitch: function () {
                return 'Ocultar/Mostrar paginaci\xF3n'
            },
            formatRefresh: function () {
                return 'Refrescar'
            },
            formatToggle: function () {
                return 'Ocultar/Mostrar'
            },
            formatColumns: function () {
                return 'Columnas'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Todos'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Exportar los datos'
            },
            formatClearFilters: function () {
                return 'Borrar los filtros'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'B\xFAsqueda avanzada'
            },
            formatAdvancedCloseButton: function () {
                return 'Cerrar'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['es-ES'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEsMX = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['es-MX'] = {
            formatLoadingMessage: function () {
                return 'Cargando, espere por favor'
            },
            formatRecordsPerPage: function (a) {
                return a + ' registros por p\xE1gina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Mostrando ' + a + ' a ' + b + ' de ' + c + ' filas'
            },
            formatDetailPagination: function (a) {
                return 'Mostrando ' + a + ' filas'
            },
            formatSearch: function () {
                return 'Buscar'
            },
            formatNoMatches: function () {
                return 'No se encontraron registros que coincidan'
            },
            formatPaginationSwitch: function () {
                return 'Mostrar/ocultar paginaci\xF3n'
            },
            formatRefresh: function () {
                return 'Actualizar'
            },
            formatToggle: function () {
                return 'Cambiar vista'
            },
            formatColumns: function () {
                return 'Columnas'
            },
            formatFullscreen: function () {
                return 'Pantalla completa'
            },
            formatAllRows: function () {
                return 'Todo'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['es-MX'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEsNI = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['es-NI'] = {
            formatLoadingMessage: function () {
                return 'Cargando, por favor espere'
            },
            formatRecordsPerPage: function (a) {
                return a + ' registros por p\xE1gina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Mostrando de ' + a + ' a ' + b + ' registros de ' + c + ' registros en total'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Buscar'
            },
            formatNoMatches: function () {
                return 'No se encontraron registros'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Refrescar'
            },
            formatToggle: function () {
                return 'Alternar'
            },
            formatColumns: function () {
                return 'Columnas'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Todo'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['es-NI'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEsSP = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['es-SP'] = {
            formatLoadingMessage: function () {
                return 'Cargando, por favor espera'
            },
            formatRecordsPerPage: function (a) {
                return a + ' registros por p&#225;gina.'
            },
            formatShowingRows: function (a, b, c) {
                return a + ' - ' + b + ' de ' + c + ' registros.'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Buscar'
            },
            formatNoMatches: function () {
                return 'No se han encontrado registros.'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Actualizar'
            },
            formatToggle: function () {
                return 'Alternar'
            },
            formatColumns: function () {
                return 'Columnas'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Todo'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['es-SP'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEtEE = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['et-EE'] = {
            formatLoadingMessage: function () {
                return 'P\xE4ring k\xE4ib, palun oota'
            },
            formatRecordsPerPage: function (a) {
                return a + ' rida lehe kohta'
            },
            formatShowingRows: function (a, b, c) {
                return 'N\xE4itan tulemusi ' + a + ' kuni ' + b + ' - kokku ' + c + ' tulemust'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Otsi'
            },
            formatNoMatches: function () {
                return 'P\xE4ringu tingimustele ei vastanud \xFChtegi tulemust'
            },
            formatPaginationSwitch: function () {
                return 'N\xE4ita/Peida lehtedeks jagamine'
            },
            formatRefresh: function () {
                return 'V\xE4rskenda'
            },
            formatToggle: function () {
                return 'L\xFClita'
            },
            formatColumns: function () {
                return 'Veerud'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'K\xF5ik'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['et-EE'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableEuEU = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['eu-EU'] = {
            formatLoadingMessage: function () {
                return 'Itxaron mesedez'
            },
            formatRecordsPerPage: function (a) {
                return a + ' emaitza orriko.'
            },
            formatShowingRows: function (a, b, c) {
                return c + ' erregistroetatik ' + a + 'etik ' + b + 'erakoak erakusten.'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Bilatu'
            },
            formatNoMatches: function () {
                return 'Ez da emaitzarik aurkitu'
            },
            formatPaginationSwitch: function () {
                return 'Ezkutatu/Erakutsi orrikatzea'
            },
            formatRefresh: function () {
                return 'Eguneratu'
            },
            formatToggle: function () {
                return 'Ezkutatu/Erakutsi'
            },
            formatColumns: function () {
                return 'Zutabeak'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Guztiak'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['eu-EU'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableFaIR = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['fa-IR'] = {
            formatLoadingMessage: function () {
                return '\u062F\u0631 \u062D\u0627\u0644 \u0628\u0627\u0631\u06AF\u0630\u0627\u0631\u06CC, \u0644\u0637\u0641\u0627 \u0635\u0628\u0631 \u06A9\u0646\u06CC\u062F'
            },
            formatRecordsPerPage: function (a) {
                return a + ' \u0631\u06A9\u0648\u0631\u062F \u062F\u0631 \u0635\u0641\u062D\u0647'
            },
            formatShowingRows: function (a, b, c) {
                return '\u0646\u0645\u0627\u06CC\u0634 ' + a + ' \u062A\u0627 ' + b + ' \u0627\u0632 ' + c + ' \u0631\u062F\u06CC\u0641'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u062C\u0633\u062A\u062C\u0648'
            },
            formatNoMatches: function () {
                return '\u0631\u06A9\u0648\u0631\u062F\u06CC \u06CC\u0627\u0641\u062A \u0646\u0634\u062F.'
            },
            formatPaginationSwitch: function () {
                return '\u0646\u0645\u0627\u06CC\u0634/\u0645\u062E\u0641\u06CC \u0635\u0641\u062D\u0647 \u0628\u0646\u062F\u06CC'
            },
            formatRefresh: function () {
                return '\u0628\u0647 \u0631\u0648\u0632 \u0631\u0633\u0627\u0646\u06CC'
            },
            formatToggle: function () {
                return '\u062A\u063A\u06CC\u06CC\u0631 \u0646\u0645\u0627\u06CC\u0634'
            },
            formatColumns: function () {
                return '\u0633\u0637\u0631 \u0647\u0627'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return '\u0647\u0645\u0647'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['fa-IR'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableFiFI = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['fi-FI'] = {
            formatLoadingMessage: function () {
                return 'Ladataan, ole hyv\xE4 ja odota'
            },
            formatRecordsPerPage: function (a) {
                return a + ' rivi\xE4 sivulla'
            },
            formatShowingRows: function (a, b, c) {
                return 'N\xE4ytet\xE4\xE4n rivit ' + a + ' - ' + b + ' / ' + c
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Hae'
            },
            formatNoMatches: function () {
                return 'Hakuehtoja vastaavia tuloksia ei l\xF6ytynyt'
            },
            formatPaginationSwitch: function () {
                return 'N\xE4yt\xE4/Piilota sivutus'
            },
            formatRefresh: function () {
                return 'P\xE4ivit\xE4'
            },
            formatToggle: function () {
                return 'Valitse'
            },
            formatColumns: function () {
                return 'Sarakkeet'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Kaikki'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Vie tiedot'
            },
            formatClearFilters: function () {
                return 'Poista suodattimet'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['fi-FI'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableFrBE = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['fr-BE'] = {
            formatLoadingMessage: function () {
                return 'Chargement en cours'
            },
            formatRecordsPerPage: function (a) {
                return a + ' entr\xE9es par page'
            },
            formatShowingRows: function (a, b, c) {
                return 'Affiche de' + a + ' \xE0 ' + b + ' sur ' + c + ' lignes'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Recherche'
            },
            formatNoMatches: function () {
                return 'Pas de fichiers trouv\xE9s'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Refresh'
            },
            formatToggle: function () {
                return 'Toggle'
            },
            formatColumns: function () {
                return 'Columns'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['fr-BE'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableFrFR = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['fr-FR'] = {
            formatLoadingMessage: function () {
                return 'Chargement en cours, patientez, s\xB4il vous pla\xEEt'
            },
            formatRecordsPerPage: function (a) {
                return a + ' lignes par page'
            },
            formatShowingRows: function (a, b, c) {
                return 'Affichage des lignes ' + a + ' \xE0 ' + b + ' sur ' + c + ' lignes au total'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Rechercher'
            },
            formatNoMatches: function () {
                return 'Aucun r\xE9sultat trouv\xE9'
            },
            formatPaginationSwitch: function () {
                return 'Montrer/Masquer pagination'
            },
            formatRefresh: function () {
                return 'Rafra\xEEchir'
            },
            formatToggle: function () {
                return 'Alterner'
            },
            formatColumns: function () {
                return 'Colonnes'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Tous'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Exporter les donn\xE9es'
            },
            formatClearFilters: function () {
                return 'Vider les filtres'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Recherche avanc\xE9e'
            },
            formatAdvancedCloseButton: function () {
                return 'Fermer'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['fr-FR'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableHeIL = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['he-IL'] = {
            formatLoadingMessage: function () {
                return '\u05D8\u05D5\u05E2\u05DF, \u05E0\u05D0 \u05DC\u05D4\u05DE\u05EA\u05D9\u05DF'
            },
            formatRecordsPerPage: function (a) {
                return a + ' \u05E9\u05D5\u05E8\u05D5\u05EA \u05D1\u05E2\u05DE\u05D5\u05D3'
            },
            formatShowingRows: function (a, b, c) {
                return '\u05DE\u05E6\u05D9\u05D2 ' + a + ' \u05E2\u05D3 ' + b + ' \u05DE-' + c + ' \u05E9\u05D5\u05E8\u05D5\u05EA'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u05D7\u05D9\u05E4\u05D5\u05E9'
            },
            formatNoMatches: function () {
                return '\u05DC\u05D0 \u05E0\u05DE\u05E6\u05D0\u05D5 \u05E8\u05E9\u05D5\u05DE\u05D5\u05EA \u05EA\u05D5\u05D0\u05DE\u05D5\u05EA'
            },
            formatPaginationSwitch: function () {
                return '\u05D4\u05E1\u05EA\u05E8/\u05D4\u05E6\u05D2 \u05DE\u05E1\u05E4\u05D5\u05E8 \u05D3\u05E4\u05D9\u05DD'
            },
            formatRefresh: function () {
                return '\u05E8\u05E2\u05E0\u05DF'
            },
            formatToggle: function () {
                return '\u05D4\u05D7\u05DC\u05E3 \u05EA\u05E6\u05D5\u05D2\u05D4'
            },
            formatColumns: function () {
                return '\u05E2\u05DE\u05D5\u05D3\u05D5\u05EA'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return '\u05D4\u05DB\u05DC'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['he-IL'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableHrHR = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['hr-HR'] = {
            formatLoadingMessage: function () {
                return 'Molimo pri\u010Dekajte'
            },
            formatRecordsPerPage: function (a) {
                return a + ' broj zapisa po stranici'
            },
            formatShowingRows: function (a, b, c) {
                return 'Prikazujem ' + a + '. - ' + b + '. od ukupnog broja zapisa ' + c
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Pretra\u017Ei'
            },
            formatNoMatches: function () {
                return 'Nije prona\u0111en niti jedan zapis'
            },
            formatPaginationSwitch: function () {
                return 'Prika\u017Ei/sakrij stranice'
            },
            formatRefresh: function () {
                return 'Osvje\u017Ei'
            },
            formatToggle: function () {
                return 'Promijeni prikaz'
            },
            formatColumns: function () {
                return 'Kolone'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Sve'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['hr-HR'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableHuHU = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['hu-HU'] = {
            formatLoadingMessage: function () {
                return 'Bet\xF6lt\xE9s, k\xE9rem v\xE1rjon'
            },
            formatRecordsPerPage: function (a) {
                return a + ' rekord per oldal'
            },
            formatShowingRows: function (a, b, c) {
                return 'Megjelen\xEDtve ' + a + ' - ' + b + ' / ' + c + ' \xF6sszesen'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Keres\xE9s'
            },
            formatNoMatches: function () {
                return 'Nincs tal\xE1lat'
            },
            formatPaginationSwitch: function () {
                return 'Lapoz\xF3 elrejt\xE9se/megjelen\xEDt\xE9se'
            },
            formatRefresh: function () {
                return 'Friss\xEDt\xE9s'
            },
            formatToggle: function () {
                return '\xD6sszecsuk/Kinyit'
            },
            formatColumns: function () {
                return 'Oszlopok'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return '\xD6sszes'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['hu-HU'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableIdID = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['id-ID'] = {
            formatLoadingMessage: function () {
                return 'Memuat, mohon tunggu'
            },
            formatRecordsPerPage: function (a) {
                return a + ' baris per halaman'
            },
            formatShowingRows: function (a, b, c) {
                return 'Menampilkan ' + a + ' sampai ' + b + ' dari ' + c + ' baris'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Pencarian'
            },
            formatNoMatches: function () {
                return 'Tidak ditemukan data yang cocok'
            },
            formatPaginationSwitch: function () {
                return 'Sembunyikan/Tampilkan halaman'
            },
            formatRefresh: function () {
                return 'Muat ulang'
            },
            formatToggle: function () {
                return 'Beralih'
            },
            formatColumns: function () {
                return 'kolom'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Semua'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Ekspor data'
            },
            formatClearFilters: function () {
                return 'Bersihkan filter'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['id-ID'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableItIT = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['it-IT'] = {
            formatLoadingMessage: function () {
                return 'Caricamento in corso'
            },
            formatRecordsPerPage: function (a) {
                return a + ' elementi per pagina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Visualizzazione da ' + a + ' a ' + b + ' di ' + c + ' elementi'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Cerca'
            },
            formatNoMatches: function () {
                return 'Nessun elemento trovato'
            },
            formatPaginationSwitch: function () {
                return 'Nascondi/Mostra paginazione'
            },
            formatRefresh: function () {
                return 'Aggiorna'
            },
            formatToggle: function () {
                return 'Attiva/Disattiva'
            },
            formatColumns: function () {
                return 'Colonne'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Tutto'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Esporta dati'
            },
            formatClearFilters: function () {
                return 'Pulisci filtri'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['it-IT'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableJaJP = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['ja-JP'] = {
            formatLoadingMessage: function () {
                return '\u8AAD\u307F\u8FBC\u307F\u4E2D\u3067\u3059\u3002\u5C11\u3005\u304A\u5F85\u3061\u304F\u3060\u3055\u3044\u3002'
            },
            formatRecordsPerPage: function (a) {
                return '\u30DA\u30FC\u30B8\u5F53\u305F\u308A\u6700\u5927' + a + '\u4EF6'
            },
            formatShowingRows: function (a, b, c) {
                return '\u5168' + c + '\u4EF6\u304B\u3089\u3001' + a + '\u304B\u3089' + b + '\u4EF6\u76EE\u307E\u3067\u8868\u793A\u3057\u3066\u3044\u307E\u3059'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u691C\u7D22'
            },
            formatNoMatches: function () {
                return '\u8A72\u5F53\u3059\u308B\u30EC\u30B3\u30FC\u30C9\u304C\u898B\u3064\u304B\u308A\u307E\u305B\u3093'
            },
            formatPaginationSwitch: function () {
                return '\u30DA\u30FC\u30B8\u6570\u3092\u8868\u793A\u30FB\u975E\u8868\u793A'
            },
            formatRefresh: function () {
                return '\u66F4\u65B0'
            },
            formatToggle: function () {
                return '\u30C8\u30B0\u30EB'
            },
            formatColumns: function () {
                return '\u5217'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return '\u3059\u3079\u3066'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['ja-JP'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableKaGE = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['ka-GE'] = {
            formatLoadingMessage: function () {
                return '\u10D8\u10E2\u10D5\u10D8\u10E0\u10D7\u10D4\u10D1\u10D0, \u10D2\u10D7\u10EE\u10DD\u10D5\u10D7 \u10DB\u10DD\u10D8\u10EA\u10D0\u10D3\u10DD\u10D7'
            },
            formatRecordsPerPage: function (a) {
                return a + ' \u10E9\u10D0\u10DC\u10D0\u10EC\u10D4\u10E0\u10D8 \u10D7\u10D8\u10D7\u10DD \u10D2\u10D5\u10D4\u10E0\u10D3\u10D6\u10D4'
            },
            formatShowingRows: function (a, b, c) {
                return '\u10DC\u10D0\u10E9\u10D5\u10D4\u10DC\u10D4\u10D1\u10D8\u10D0 ' + a + '-\u10D3\u10D0\u10DC ' + b + '-\u10DB\u10D3\u10D4 \u10E9\u10D0\u10DC\u10D0\u10EC\u10D4\u10E0\u10D8 \u10EF\u10D0\u10DB\u10E3\u10E0\u10D8 ' + c + '-\u10D3\u10D0\u10DC'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u10EB\u10D4\u10D1\u10DC\u10D0'
            },
            formatNoMatches: function () {
                return '\u10DB\u10DD\u10DC\u10D0\u10EA\u10D4\u10DB\u10D4\u10D1\u10D8 \u10D0\u10E0 \u10D0\u10E0\u10D8\u10E1'
            },
            formatPaginationSwitch: function () {
                return '\u10D2\u10D5\u10D4\u10E0\u10D3\u10D4\u10D1\u10D8\u10E1 \u10D2\u10D0\u10D3\u10D0\u10DB\u10E0\u10D7\u10D5\u10D4\u10DA\u10D8\u10E1 \u10D3\u10D0\u10DB\u10D0\u10DA\u10D5\u10D0/\u10D2\u10D0\u10DB\u10DD\u10E9\u10D4\u10DC\u10D0'
            },
            formatRefresh: function () {
                return '\u10D2\u10D0\u10DC\u10D0\u10EE\u10DA\u10D4\u10D1\u10D0'
            },
            formatToggle: function () {
                return '\u10E9\u10D0\u10E0\u10D7\u10D5\u10D0/\u10D2\u10D0\u10DB\u10DD\u10E0\u10D7\u10D5\u10D0'
            },
            formatColumns: function () {
                return '\u10E1\u10D5\u10D4\u10E2\u10D4\u10D1\u10D8'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['ka-GE'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableKoKR = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['ko-KR'] = {
            formatLoadingMessage: function () {
                return '\uB370\uC774\uD130\uB97C \uBD88\uB7EC\uC624\uB294 \uC911\uC785\uB2C8\uB2E4'
            },
            formatRecordsPerPage: function (a) {
                return '\uD398\uC774\uC9C0 \uB2F9 ' + a + '\uAC1C \uB370\uC774\uD130 \uCD9C\uB825'
            },
            formatShowingRows: function (a, b, c) {
                return '\uC804\uCCB4 ' + c + '\uAC1C \uC911 ' + a + '~' + b + '\uBC88\uC9F8 \uB370\uC774\uD130 \uCD9C\uB825,'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\uAC80\uC0C9'
            },
            formatNoMatches: function () {
                return '\uC870\uD68C\uB41C \uB370\uC774\uD130\uAC00 \uC5C6\uC2B5\uB2C8\uB2E4.'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return '\uC0C8\uB85C \uACE0\uCE68'
            },
            formatToggle: function () {
                return '\uC804\uD658'
            },
            formatColumns: function () {
                return '\uCEEC\uB7FC \uD544\uD130\uB9C1'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['ko-KR'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableMsMY = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['ms-MY'] = {
            formatLoadingMessage: function () {
                return 'Permintaan sedang dimuatkan. Sila tunggu sebentar'
            },
            formatRecordsPerPage: function (a) {
                return a + ' rekod setiap muka surat'
            },
            formatShowingRows: function (a, b, c) {
                return 'Sedang memaparkan rekod ' + a + ' hingga ' + b + ' daripada jumlah ' + c + ' rekod'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Cari'
            },
            formatNoMatches: function () {
                return 'Tiada rekod yang menyamai permintaan'
            },
            formatPaginationSwitch: function () {
                return 'Tunjuk/sembunyi muka surat'
            },
            formatRefresh: function () {
                return 'Muatsemula'
            },
            formatToggle: function () {
                return 'Tukar'
            },
            formatColumns: function () {
                return 'Lajur'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Semua'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['ms-MY'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableNbNO = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['nb-NO'] = {
            formatLoadingMessage: function () {
                return 'Oppdaterer, vennligst vent'
            },
            formatRecordsPerPage: function (a) {
                return a + ' poster pr side'
            },
            formatShowingRows: function (a, b, c) {
                return 'Viser ' + a + ' til ' + b + ' av ' + c + ' rekker'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'S\xF8k'
            },
            formatNoMatches: function () {
                return 'Ingen poster funnet'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Oppdater'
            },
            formatToggle: function () {
                return 'Endre'
            },
            formatColumns: function () {
                return 'Kolonner'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['nb-NO'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableNlNL = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['nl-NL'] = {
            formatLoadingMessage: function () {
                return 'Laden, even geduld'
            },
            formatRecordsPerPage: function (a) {
                return a + ' records per pagina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Toon ' + a + ' tot ' + b + ' van ' + c + ' record' + (1 < c ? 's' : '')
            },
            formatDetailPagination: function (a) {
                return 'Toon ' + a + ' record' + (1 < a ? 's' : '')
            },
            formatSearch: function () {
                return 'Zoeken'
            },
            formatNoMatches: function () {
                return 'Geen resultaten gevonden'
            },
            formatPaginationSwitch: function () {
                return 'Verberg/Toon paginatie'
            },
            formatRefresh: function () {
                return 'Vernieuwen'
            },
            formatToggle: function () {
                return 'Omschakelen'
            },
            formatColumns: function () {
                return 'Kolommen'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Alle'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Exporteer data'
            },
            formatClearFilters: function () {
                return 'Verwijder filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['nl-NL'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTablePlPL = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['pl-PL'] = {
            formatLoadingMessage: function () {
                return '\u0141adowanie, prosz\u0119 czeka\u0107'
            },
            formatRecordsPerPage: function (a) {
                return a + ' rekord\xF3w na stron\u0119'
            },
            formatShowingRows: function (a, b, c) {
                return 'Wy\u015Bwietlanie rekord\xF3w od ' + a + ' do ' + b + ' z ' + c
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Szukaj'
            },
            formatNoMatches: function () {
                return 'Niestety, nic nie znaleziono'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Od\u015Bwie\u017C'
            },
            formatToggle: function () {
                return 'Prze\u0142\u0105cz'
            },
            formatColumns: function () {
                return 'Kolumny'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['pl-PL'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTablePtBR = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['pt-BR'] = {
            formatLoadingMessage: function () {
                return 'Carregando, aguarde'
            },
            formatRecordsPerPage: function (a) {
                return a + ' registros por p\xE1gina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Exibindo ' + a + ' at\xE9 ' + b + ' de ' + c + ' linhas'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Pesquisar'
            },
            formatNoMatches: function () {
                return 'Nenhum registro encontrado'
            },
            formatPaginationSwitch: function () {
                return 'Ocultar/Exibir pagina\xE7\xE3o'
            },
            formatRefresh: function () {
                return 'Recarregar'
            },
            formatToggle: function () {
                return 'Alternar'
            },
            formatColumns: function () {
                return 'Colunas'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['pt-BR'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTablePtPT = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['pt-PT'] = {
            formatLoadingMessage: function () {
                return 'A carregar, por favor aguarde'
            },
            formatRecordsPerPage: function (a) {
                return a + ' registos por p&aacute;gina'
            },
            formatShowingRows: function (a, b, c) {
                return 'A mostrar ' + a + ' at&eacute; ' + b + ' de ' + c + ' linhas'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Pesquisa'
            },
            formatNoMatches: function () {
                return 'Nenhum registo encontrado'
            },
            formatPaginationSwitch: function () {
                return 'Esconder/Mostrar pagina&ccedil&atilde;o'
            },
            formatRefresh: function () {
                return 'Atualizar'
            },
            formatToggle: function () {
                return 'Alternar'
            },
            formatColumns: function () {
                return 'Colunas'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Tudo'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['pt-PT'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableRoRO = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['ro-RO'] = {
            formatLoadingMessage: function () {
                return 'Se incarca, va rugam asteptati'
            },
            formatRecordsPerPage: function (a) {
                return a + ' inregistrari pe pagina'
            },
            formatShowingRows: function (a, b, c) {
                return 'Arata de la ' + a + ' pana la ' + b + ' din ' + c + ' randuri'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Cauta'
            },
            formatNoMatches: function () {
                return 'Nu au fost gasite inregistrari'
            },
            formatPaginationSwitch: function () {
                return 'Ascunde/Arata paginatia'
            },
            formatRefresh: function () {
                return 'Reincarca'
            },
            formatToggle: function () {
                return 'Comuta'
            },
            formatColumns: function () {
                return 'Coloane'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Toate'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['ro-RO'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableRuRU = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['ru-RU'] = {
            formatLoadingMessage: function () {
                return '\u041F\u043E\u0436\u0430\u043B\u0443\u0439\u0441\u0442\u0430, \u043F\u043E\u0434\u043E\u0436\u0434\u0438\u0442\u0435, \u0438\u0434\u0451\u0442 \u0437\u0430\u0433\u0440\u0443\u0437\u043A\u0430'
            },
            formatRecordsPerPage: function (a) {
                return a + ' \u0437\u0430\u043F\u0438\u0441\u0435\u0439 \u043D\u0430 \u0441\u0442\u0440\u0430\u043D\u0438\u0446\u0443'
            },
            formatShowingRows: function (a, b, c) {
                return '\u0417\u0430\u043F\u0438\u0441\u0438 \u0441 ' + a + ' \u043F\u043E ' + b + ' \u0438\u0437 ' + c
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u041F\u043E\u0438\u0441\u043A'
            },
            formatNoMatches: function () {
                return '\u041D\u0438\u0447\u0435\u0433\u043E \u043D\u0435 \u043D\u0430\u0439\u0434\u0435\u043D\u043E'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return '\u041E\u0431\u043D\u043E\u0432\u0438\u0442\u044C'
            },
            formatToggle: function () {
                return '\u041F\u0435\u0440\u0435\u043A\u043B\u044E\u0447\u0438\u0442\u044C'
            },
            formatColumns: function () {
                return '\u041A\u043E\u043B\u043E\u043D\u043A\u0438'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return '\u041E\u0447\u0438\u0441\u0442\u0438\u0442\u044C \u0444\u0438\u043B\u044C\u0442\u0440\u044B'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['ru-RU'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableSkSK = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['sk-SK'] = {
            formatLoadingMessage: function () {
                return 'Pros\xEDm \u010Dakajte'
            },
            formatRecordsPerPage: function (a) {
                return a + ' z\xE1znamov na stranu'
            },
            formatShowingRows: function (a, b, c) {
                return 'Zobrazen\xE1 ' + a + '. - ' + b + '. polo\u017Eka z celkov\xFDch ' + c
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Vyh\u013Ead\xE1vanie'
            },
            formatNoMatches: function () {
                return 'Nen\xE1jden\xE1 \u017Eiadna vyhovuj\xFAca polo\u017Eka'
            },
            formatPaginationSwitch: function () {
                return 'Skry/Zobraz str\xE1nkovanie'
            },
            formatRefresh: function () {
                return 'Obnovi\u0165'
            },
            formatToggle: function () {
                return 'Prepni'
            },
            formatColumns: function () {
                return 'St\u013Apce'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'V\u0161etky'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Exportuj d\xE1ta'
            },
            formatClearFilters: function () {
                return 'Odstr\xE1\u0148 filtre'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['sk-SK'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableSvSE = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['sv-SE'] = {
            formatLoadingMessage: function () {
                return 'Laddar, v\xE4nligen v\xE4nta'
            },
            formatRecordsPerPage: function (a) {
                return a + ' rader per sida'
            },
            formatShowingRows: function (a, b, c) {
                return 'Visa ' + a + ' till ' + b + ' av ' + c + ' rader'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'S\xF6k'
            },
            formatNoMatches: function () {
                return 'Inga matchande resultat funna.'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Uppdatera'
            },
            formatToggle: function () {
                return 'Skifta'
            },
            formatColumns: function () {
                return 'kolumn'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['sv-SE'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableThTH = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['th-TH'] = {
            formatLoadingMessage: function () {
                return '\u0E01\u0E33\u0E25\u0E31\u0E07\u0E42\u0E2B\u0E25\u0E14\u0E02\u0E49\u0E2D\u0E21\u0E39\u0E25, \u0E01\u0E23\u0E38\u0E13\u0E32\u0E23\u0E2D\u0E2A\u0E31\u0E01\u0E04\u0E23\u0E39\u0E48'
            },
            formatRecordsPerPage: function (a) {
                return a + ' \u0E23\u0E32\u0E22\u0E01\u0E32\u0E23\u0E15\u0E48\u0E2D\u0E2B\u0E19\u0E49\u0E32'
            },
            formatShowingRows: function (a, b, c) {
                return '\u0E23\u0E32\u0E22\u0E01\u0E32\u0E23\u0E17\u0E35\u0E48 ' + a + ' \u0E16\u0E36\u0E07 ' + b + ' \u0E08\u0E32\u0E01\u0E17\u0E31\u0E49\u0E07\u0E2B\u0E21\u0E14 ' + c + ' \u0E23\u0E32\u0E22\u0E01\u0E32\u0E23'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u0E04\u0E49\u0E19\u0E2B\u0E32'
            },
            formatNoMatches: function () {
                return '\u0E44\u0E21\u0E48\u0E1E\u0E1A\u0E23\u0E32\u0E22\u0E01\u0E32\u0E23\u0E17\u0E35\u0E48\u0E04\u0E49\u0E19\u0E2B\u0E32 !'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return '\u0E23\u0E35\u0E40\u0E1F\u0E23\u0E2A'
            },
            formatToggle: function () {
                return '\u0E2A\u0E25\u0E31\u0E1A\u0E21\u0E38\u0E21\u0E21\u0E2D\u0E07'
            },
            formatColumns: function () {
                return '\u0E04\u0E2D\u0E25\u0E31\u0E21\u0E19\u0E4C'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['th-TH'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableTrTR = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['tr-TR'] = {
            formatLoadingMessage: function () {
                return 'Y\xFCkleniyor, l\xFCtfen bekleyin'
            },
            formatRecordsPerPage: function (a) {
                return 'Sayfa ba\u015F\u0131na ' + a + ' kay\u0131t.'
            },
            formatShowingRows: function (a, b, c) {
                return c + ' kay\u0131ttan ' + a + '-' + b + ' aras\u0131 g\xF6steriliyor.'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Ara'
            },
            formatNoMatches: function () {
                return 'E\u015Fle\u015Fen kay\u0131t bulunamad\u0131.'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Yenile'
            },
            formatToggle: function () {
                return 'De\u011Fi\u015Ftir'
            },
            formatColumns: function () {
                return 'S\xFCtunlar'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'T\xFCm Sat\u0131rlar'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['tr-TR'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableUkUA = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['uk-UA'] = {
            formatLoadingMessage: function () {
                return '\u0417\u0430\u0432\u0430\u043D\u0442\u0430\u0436\u0435\u043D\u043D\u044F, \u0431\u0443\u0434\u044C \u043B\u0430\u0441\u043A\u0430, \u0437\u0430\u0447\u0435\u043A\u0430\u0439\u0442\u0435'
            },
            formatRecordsPerPage: function (a) {
                return a + ' \u0437\u0430\u043F\u0438\u0441\u0456\u0432 \u043D\u0430 \u0441\u0442\u043E\u0440\u0456\u043D\u043A\u0443'
            },
            formatShowingRows: function (a, b, c) {
                return '\u041F\u043E\u043A\u0430\u0437\u0430\u043D\u043E \u0437 ' + a + ' \u043F\u043E ' + b + '. \u0412\u0441\u044C\u043E\u0433\u043E: ' + c
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u041F\u043E\u0448\u0443\u043A'
            },
            formatNoMatches: function () {
                return '\u041D\u0435 \u0437\u043D\u0430\u0439\u0434\u0435\u043D\u043E \u0436\u043E\u0434\u043D\u043E\u0433\u043E \u0437\u0430\u043F\u0438\u0441\u0443'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return '\u041E\u043D\u043E\u0432\u0438\u0442\u0438'
            },
            formatToggle: function () {
                return '\u0417\u043C\u0456\u043D\u0438\u0442\u0438'
            },
            formatColumns: function () {
                return '\u0421\u0442\u043E\u0432\u043F\u0446\u0456'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return '\u041E\u0447\u0438\u0441\u0442\u0438\u0442\u0438 \u0444\u0456\u043B\u044C\u0442\u0440\u0438'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['uk-UA'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableUrPK = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['ur-PK'] = {
            formatLoadingMessage: function () {
                return '\u0628\u0631\u0627\u06D3 \u0645\u06C1\u0631\u0628\u0627\u0646\u06CC \u0627\u0646\u062A\u0638\u0627\u0631 \u06A9\u06CC\u062C\u0626\u06D2'
            },
            formatRecordsPerPage: function (a) {
                return a + ' \u0631\u06CC\u06A9\u0627\u0631\u0688\u0632 \u0641\u06CC \u0635\u0641\u06C1 '
            },
            formatShowingRows: function (a, b, c) {
                return '\u062F\u06CC\u06A9\u06BE\u06CC\u06BA ' + a + ' \u0633\u06D2 ' + b + ' \u06A9\u06D2 ' + c + '\u0631\u06CC\u06A9\u0627\u0631\u0688\u0632'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return '\u062A\u0644\u0627\u0634'
            },
            formatNoMatches: function () {
                return '\u06A9\u0648\u0626\u06CC \u0631\u06CC\u06A9\u0627\u0631\u0688 \u0646\u06C1\u06CC\u06BA \u0645\u0644\u0627'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return '\u062A\u0627\u0632\u06C1 \u06A9\u0631\u06CC\u06BA'
            },
            formatToggle: function () {
                return '\u062A\u0628\u062F\u06CC\u0644 \u06A9\u0631\u06CC\u06BA'
            },
            formatColumns: function () {
                return '\u06A9\u0627\u0644\u0645'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['ur-PK'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableUzLatnUZ = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['uz-Latn-UZ'] = {
            formatLoadingMessage: function () {
                return 'Yuklanyapti, iltimos kuting'
            },
            formatRecordsPerPage: function (a) {
                return a + ' qator har sahifada'
            },
            formatShowingRows: function (a, b, c) {
                return 'Ko\'rsatypati ' + a + ' dan ' + b + ' gacha ' + c + ' qatorlarni'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'Qidirish'
            },
            formatNoMatches: function () {
                return 'Hech narsa topilmadi'
            },
            formatPaginationSwitch: function () {
                return 'Sahifalashni yashirish/ko\'rsatish'
            },
            formatRefresh: function () {
                return 'Yangilash'
            },
            formatToggle: function () {
                return 'Ko\'rinish'
            },
            formatColumns: function () {
                return 'Ustunlar'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'Hammasi'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Eksport'
            },
            formatClearFilters: function () {
                return 'Filtrlarni tozalash'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['uz-Latn-UZ'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableViVN = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['vi-VN'] = {
            formatLoadingMessage: function () {
                return '\u0110ang t\u1EA3i'
            },
            formatRecordsPerPage: function (a) {
                return a + ' b\u1EA3n ghi m\u1ED7i trang'
            },
            formatShowingRows: function (a, b, c) {
                return 'Hi\u1EC3n th\u1ECB t\u1EEB trang ' + a + ' \u0111\u1EBFn ' + b + ' c\u1EE7a ' + c + ' b\u1EA3ng ghi'
            },
            formatDetailPagination: function (a) {
                return 'Showing ' + a + ' rows'
            },
            formatSearch: function () {
                return 'T\xECm ki\u1EBFm'
            },
            formatNoMatches: function () {
                return 'Kh\xF4ng c\xF3 d\u1EEF li\u1EC7u'
            },
            formatPaginationSwitch: function () {
                return 'Hide/Show pagination'
            },
            formatRefresh: function () {
                return 'Refresh'
            },
            formatToggle: function () {
                return 'Toggle'
            },
            formatColumns: function () {
                return 'Columns'
            },
            formatFullscreen: function () {
                return 'Fullscreen'
            },
            formatAllRows: function () {
                return 'All'
            },
            formatAutoRefresh: function () {
                return 'Auto Refresh'
            },
            formatExport: function () {
                return 'Export data'
            },
            formatClearFilters: function () {
                return 'Clear filters'
            },
            formatJumpto: function () {
                return 'GO'
            },
            formatAdvancedSearch: function () {
                return 'Advanced search'
            },
            formatAdvancedCloseButton: function () {
                return 'Close'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['vi-VN'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableZhCN = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['zh-CN'] = {
            formatLoadingMessage: function () {
                return '\u6B63\u5728\u52AA\u529B\u5730\u52A0\u8F7D\u6570\u636E\u4E2D\uFF0C\u8BF7\u7A0D\u5019'
            },
            formatRecordsPerPage: function (a) {
                return '\u6BCF\u9875\u663E\u793A ' + a + ' \u6761\u8BB0\u5F55'
            },
            formatShowingRows: function (a, b, c) {
                return '\u663E\u793A\u7B2C ' + a + ' \u5230\u7B2C ' + b + ' \u6761\u8BB0\u5F55\uFF0C\u603B\u5171 ' + c + ' \u6761\u8BB0\u5F55'
            },
            formatDetailPagination: function (a) {
                return '\u603B\u5171 ' + a + ' \u6761\u8BB0\u5F55'
            },
            formatSearch: function () {
                return '\u641C\u7D22'
            },
            formatNoMatches: function () {
                return '\u6CA1\u6709\u627E\u5230\u5339\u914D\u7684\u8BB0\u5F55'
            },
            formatPaginationSwitch: function () {
                return '\u9690\u85CF/\u663E\u793A\u5206\u9875'
            },
            formatRefresh: function () {
                return '\u5237\u65B0'
            },
            formatToggle: function () {
                return '\u5207\u6362'
            },
            formatColumns: function () {
                return '\u5217'
            },
            formatFullscreen: function () {
                return '\u5168\u5C4F'
            },
            formatAllRows: function () {
                return '\u6240\u6709'
            },
            formatAutoRefresh: function () {
                return '\u81EA\u52A8\u5237\u65B0'
            },
            formatExport: function () {
                return '\u5BFC\u51FA\u6570\u636E'
            },
            formatClearFilters: function () {
                return '\u6E05\u7A7A\u8FC7\u6EE4'
            },
            formatJumpto: function () {
                return '\u8DF3\u8F6C'
            },
            formatAdvancedSearch: function () {
                return '\u9AD8\u7EA7\u641C\u7D22'
            },
            formatAdvancedCloseButton: function () {
                return '\u5173\u95ED'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['zh-CN'])
    })(jQuery)
});
(function (a, b) {
    if ('function' == typeof define && define.amd) define([], b);
    else if ('undefined' != typeof exports) b();
    else {
        b(), a.bootstrapTableZhTW = {
            exports: {}
        }.exports
    }
})(this, function () {
    'use strict';
    (function (a) {
        a.fn.bootstrapTable.locales['zh-TW'] = {
            formatLoadingMessage: function () {
                return '\u6B63\u5728\u52AA\u529B\u5730\u8F09\u5165\u8CC7\u6599\uFF0C\u8ACB\u7A0D\u5019'
            },
            formatRecordsPerPage: function (a) {
                return '\u6BCF\u9801\u986F\u793A ' + a + ' \u9805\u8A18\u9304'
            },
            formatShowingRows: function (a, b, c) {
                return '\u986F\u793A\u7B2C ' + a + ' \u5230\u7B2C ' + b + ' \u9805\uFF0C\u7E3D\u5171 ' + c + ' \u9805'
            },
            formatDetailPagination: function (a) {
                return '\u7E3D\u5171 ' + a + ' \u9805\u8A18\u9304'
            },
            formatSearch: function () {
                return '\u641C\u5C0B'
            },
            formatNoMatches: function () {
                return '\u6C92\u6709\u627E\u5230\u7B26\u5408\u7684\u7D50\u679C'
            },
            formatPaginationSwitch: function () {
                return '\u96B1\u85CF/\u986F\u793A\u5206\u9801'
            },
            formatRefresh: function () {
                return '\u91CD\u65B0\u6574\u7406'
            },
            formatToggle: function () {
                return '\u5207\u63DB'
            },
            formatColumns: function () {
                return '\u5217'
            },
            formatFullscreen: function () {
                return '\u5168\u5C4F'
            },
            formatAllRows: function () {
                return '\u6240\u6709'
            },
            formatAutoRefresh: function () {
                return '\u81EA\u52D5\u5237\u65B0'
            },
            formatExport: function () {
                return '\u5C0E\u51FA\u6578\u64DA'
            },
            formatClearFilters: function () {
                return '\u6E05\u7A7A\u904E\u6FFE'
            },
            formatJumpto: function () {
                return '\u8DF3\u8F49'
            },
            formatAdvancedSearch: function () {
                return '\u9AD8\u7D1A\u641C\u5C0B'
            },
            formatAdvancedCloseButton: function () {
                return '\u95DC\u9589'
            }
        }, a.extend(a.fn.bootstrapTable.defaults, a.fn.bootstrapTable.locales['zh-TW'])
    })(jQuery)
});