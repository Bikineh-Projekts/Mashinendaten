// ~/jquery/JS/Planung.js
(function () {
    'use strict';

    function collectRows() {
        const rows = [];
        document.querySelectorAll('#tbodyPlanung tr:not([id])').forEach(tr => {
            const get = (name) => tr.querySelector(`[data-field="${name}"], input[name*=".${name}"]`);

            const artikel = get('Artikel')?.value?.trim() || null;
            const sollmenge = toInt(get('Sollmenge')?.value);
            const starten = get('Starten')?.value || null;
            const stoppen = get('Stoppen')?.value || null;
            const pause = toInt(get('Pause')?.value);
            const mhd = get('MHD')?.value || null;
            const kartonsanzahl = get('Kartonsanzahl')?.value?.trim() || null;
            const personalIst = toInt(get('PersonalIst')?.value);
            const fertigware = toInt(get('Fertigware')?.value);

            // Nur Zeilen hinzufügen, die nicht komplett leer sind
            if (artikel || sollmenge || starten || stoppen || pause || mhd || kartonsanzahl || personalIst || fertigware) {
                rows.push({
                    Artikel: artikel,
                    Sollmenge: sollmenge,
                    Starten: parseTimeSpan(starten),
                    Stoppen: parseTimeSpan(stoppen),
                    Pause: pause,
                    MHD: parseDate(mhd),
                    Kartonsanzahl: kartonsanzahl,
                    PersonalIst: personalIst,
                    Fertigware: fertigware
                });
            }
        });
        return rows;
    }

    function toInt(v) {
        if (v === null || v === undefined || v === '') return null;
        const n = parseInt(v, 10);
        return Number.isFinite(n) ? n : null;
    }

    function parseTimeSpan(timeString) {
        if (!timeString) return null;
        // HTML time input format: "HH:MM" -> TimeSpan format für C#
        const match = timeString.match(/^(\d{1,2}):(\d{2})$/);
        if (match) {
            return `${match[1].padStart(2, '0')}:${match[2]}:00`;
        }
        return timeString;
    }

    function parseDate(dateString) {
        if (!dateString) return null;
        // HTML date input format: "YYYY-MM-DD" 
        return dateString;
    }

    function addProductionRow() {
        const tbody = document.getElementById('tbodyPlanung');
        const template = document.getElementById('tplRow');

        if (!template) {
            console.error('Template für Produktionszeile nicht gefunden!');
            return;
        }

        const idx = nextIndex();
        const html = template.innerHTML.replaceAll('__idx__', String(idx));
        tbody.insertAdjacentHTML('beforeend', html);
    }

    function nextIndex() {
        const inputs = document.querySelectorAll('#tbodyPlanung input[name^="Item2["][name$="].Artikel"]');
        let max = -1;
        inputs.forEach(i => {
            const m = i.name.match(/^Item2\[(\d+)\]\.Artikel$/);
            if (m) max = Math.max(max, parseInt(m[1], 10));
        });
        return max + 1;
    }

    // Hauptinitialisierung
    document.addEventListener('DOMContentLoaded', function () {
        const form = document.getElementById('planungForm');
        if (!form) {
            console.error('Formular mit ID "planungForm" nicht gefunden!');
            return;
        }

        // Hidden Field für JSON-Daten erstellen falls nicht vorhanden
        let rowsJsonField = document.getElementById('RowsJson');
        if (!rowsJsonField) {
            rowsJsonField = document.createElement('input');
            rowsJsonField.type = 'hidden';
            rowsJsonField.id = 'RowsJson';
            rowsJsonField.name = 'RowsJson';
            form.appendChild(rowsJsonField);
        }

        // Form Submit Handler
        form.addEventListener('submit', function (e) {
            try {
                const rowsJson = JSON.stringify(collectRows());

                console.log('Collected rows:', rowsJson);

                document.getElementById('RowsJson').value = rowsJson;
            } catch (error) {
                console.error('Fehler beim Sammeln der Formulardaten:', error);
                e.preventDefault();
                alert('Fehler beim Verarbeiten der Formulardaten. Bitte überprüfen Sie Ihre Eingaben.');
            }
        });

        // Button Event Handler
        const btnAdd = document.querySelector('.js-add-prod');
        if (btnAdd) {
            btnAdd.addEventListener('click', addProductionRow);
        }

        // Event Delegation für dynamisch hinzugefügte Remove-Buttons
        const tbody = document.getElementById('tbodyPlanung');
        if (tbody) {
            tbody.addEventListener('click', function (e) {
                if (e.target.matches('.js-del-row') || e.target.closest('.js-del-row')) {
                    e.target.closest('tr')?.remove();
                }
            });
        }

        // Auto-fill heute's Datum falls leer
        const dateInput = document.querySelector('input[name="Item1.Datum"], #Item1_Datum');
        if (dateInput && !dateInput.value) {
            const today = new Date();
            const yyyy = today.getFullYear();
            const mm = String(today.getMonth() + 1).padStart(2, '0');
            const dd = String(today.getDate()).padStart(2, '0');
            dateInput.value = `${yyyy}-${mm}-${dd}`;
        }

        // Alert Auto-Hide
        setTimeout(() => {
            document.querySelectorAll('.alert-modern, .alert').forEach(alert => {
                alert.style.transition = 'opacity 0.5s';
                alert.style.opacity = '0';
                setTimeout(() => alert.remove(), 500);
            });
        }, 5000);

        // Reset Form Funktion global verfügbar machen
        window.resetForm = function () {
            if (confirm('Alle Daten zurücksetzen? Diese Aktion kann nicht rückgängig gemacht werden.')) {
                form.reset();

                // Tabelle leeren
                const tbodyPlanung = document.getElementById('tbodyPlanung');
                if (tbodyPlanung) {
                    tbodyPlanung.innerHTML = '';
                    addProductionRow();
                }

                // Datum wieder auf heute setzen
                if (dateInput) {
                    const today = new Date();
                    const yyyy = today.getFullYear();
                    const mm = String(today.getMonth() + 1).padStart(2, '0');
                    const dd = String(today.getDate()).padStart(2, '0');
                    dateInput.value = `${yyyy}-${mm}-${dd}`;
                }
            }
        };

        // Mindestens eine Produktionszeile sicherstellen
        if (tbody && tbody.children.length === 0) {
            addProductionRow();
        }
    });

})();