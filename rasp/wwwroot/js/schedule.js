const editBtn = document.getElementById('editBtn');
const addBtn = document.getElementById('addBtn');
const dayFilter = document.getElementById('dayFilter');
const groupFilter = document.getElementById('groupFilter');
const addDialog = document.getElementById('addDialog');
const cancelAdd = document.getElementById('cancelAdd');
const addForm = document.getElementById('addForm');
const lessonsContainer = document.getElementById('lessonsContainer');
const addLessonRowBtn = document.getElementById('addLessonRow');

function applyFilters() {
    const dayVal = dayFilter.value;
    const grpVal = groupFilter.value;
    document.querySelectorAll('.day-section').forEach(sec => {
        sec.style.display = (dayVal === 'all' || sec.dataset.day === dayVal) ? '' : 'none';
    });
    document.querySelectorAll('.group-card').forEach(card => {
        card.style.display = (grpVal === 'all' || card.dataset.group === grpVal) ? '' : 'none';
    });
}

function renumberRows() {
    const rows = lessonsContainer.querySelectorAll('.lesson-row');
    rows.forEach((row, index) => {

        const numberInput = row.querySelector('input[type="number"]');
        numberInput.value = index + 1;

        const inputs = row.querySelectorAll('input');
        inputs.forEach(input => {
            const name = input.getAttribute('name');
            if (name) {
                input.setAttribute('name', name.replace(/\[\d+\]/, `[${index}]`));
            }
        });
    });

    const numbers = new Set();
    rows.forEach(row => {
        const num = row.querySelector('input[type="number"]').value;
        if (numbers.has(num)) {
            alert(`Ошибка: Пара с номером ${num} уже существует!`);
            row.querySelector('input[type="number"]').focus();
        }
        numbers.add(num);
    });
}

dayFilter.addEventListener('change', applyFilters);
groupFilter.addEventListener('change', applyFilters);
window.addEventListener('load', applyFilters);

editBtn.addEventListener('click', () => {
    const isEditMode = document.body.classList.toggle('edit-mode');
    editBtn.textContent = isEditMode ? 'Выйти из редактирования' : 'Редактировать';
});


const lessonDateInput = document.getElementById('lessonDate');
const dayOfWeekDisplay = document.getElementById('dayOfWeekDisplay');

const hiddenDayInput = document.createElement('input');
hiddenDayInput.type = 'hidden';
hiddenDayInput.name = 'DayOfWeek';
addForm.appendChild(hiddenDayInput);

function updateDayOfWeekDisplay(dateStr) {
    if (!dateStr) {
        dayOfWeekDisplay.textContent = '';
        hiddenDayInput.value = '';
        return;
    }

    const date = new Date(dateStr);
    const ruDays = ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'];
    const dayIndex = date.getDay();

    dayOfWeekDisplay.textContent = `День недели: ${ruDays[dayIndex]}`;
    hiddenDayInput.value = ruDays[dayIndex];
}

lessonDateInput.addEventListener('input', () => {
    updateDayOfWeekDisplay(lessonDateInput.value);
});

addBtn.addEventListener('click', () => {
    lessonDateInput.value = '';
    updateDayOfWeekDisplay('');
});

document.querySelectorAll('.remove-day').forEach(span => {
    span.addEventListener('click', async () => {
        const card = span.closest('.group-card');
        const group = card.dataset.group;
        const date = card.dataset.date;
        const day = card.closest('.day-section').dataset.day;

        const confirmDelete = confirm(`Вы уверены, что хотите удалить расписание группы "${group}" на день "${day}" (${date})?`);
        if (!confirmDelete) return;

        const payload = { group, day, date };

        try {
            const response = await fetch('/RaspisAdmin/DeleteDay', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
                },
                body: JSON.stringify(payload)
            });

            if (response.ok) {
                console.log('Удаление прошло успешно');
                card.remove();
            } else {
                const err = await response.text();
                console.error('Ошибка при удалении:', err);
            }
        } catch (error) {
            console.error('Ошибка сети:', error);
        }
    });
});


addBtn.addEventListener('click', () => addDialog.showModal());
cancelAdd.addEventListener('click', () => addDialog.close());

addForm.addEventListener('submit', async function (e) {
    e.preventDefault();

    const formData = new FormData(addForm);
    const dto = {
        Date: formData.get('Date'),
        GroupName: formData.get('GroupName'),
        DayOfWeek: formData.get('DayOfWeek'),
        Lessons: []
    };

    const rows = lessonsContainer.querySelectorAll('.lesson-row');
    rows.forEach(row => {
        dto.Lessons.push({
            Number: row.querySelector('input[name*=".Number"]').value,
            DisciplineCode: row.querySelector('input[name*=".DisciplineCode"]').value,
            PlaceName: row.querySelector('input[name*=".PlaceName"]').value,
            TeacherName: row.querySelector('input[name*=".TeacherName"]').value
        });
    });

    try {
        const response = await fetch(addForm.action, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
            },
            body: JSON.stringify(dto)
        });

        const result = await response.json();
        if (result.success) {
            alert("Расписание успешно добавлено.");
            location.reload();
        } else {
            if (confirm(result.message + " Добавить всё равно?")) {
                await fetch(addForm.action + '?force=true', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]')?.value || ''
                    },
                    body: JSON.stringify(dto)
                });
                location.reload();
            }
        }

    } catch (error) {
        alert("Произошла ошибка при добавлении расписания.");
        console.error(error);
    }
});


let lessonIndex = 1; 

addLessonRowBtn.addEventListener('click', function () {
    const rows = lessonsContainer.querySelectorAll('.lesson-row');
    if (rows.length >= 10) {
        alert('Достигнуто максимальное количество пар (10)');
        return;
    }
    const newIndex = rows.length;

    const newRow = document.createElement('div');
    newRow.className = 'lesson-row';
    newRow.innerHTML = `
            <div>
                <label>№ пары</label>
                <input type="number" name="Lessons[${newIndex}].Number" min="1" max="10" value="${newIndex + 1}" required readonly />
            </div>
            <div>
                <label>Дисциплина</label>
                <input type="text" name="Lessons[${newIndex}].DisciplineCode" required />
            </div>
            <div>
                <label>Аудитория</label>
                <input type="text" name="Lessons[${newIndex}].PlaceName" required />
            </div>
            <div>
                <label>Преподаватель</label>
                <input type="text" name="Lessons[${newIndex}].TeacherName" required />
            </div>
            <button type="button" class="delete-row-btn">×</button>
        `;

    lessonsContainer.appendChild(newRow);

    newRow.querySelector('.delete-row-btn').addEventListener('click', function () {
        newRow.remove();
        renumberRows();
    });
});

document.querySelector('.lesson-row .delete-row-btn')?.addEventListener('click', function () {
    const row = this.closest('.lesson-row');
    if (lessonsContainer.querySelectorAll('.lesson-row').length > 1) {
        row.remove();
        renumberRows();
    } else {
        alert('Должна остаться хотя бы одна пара!');
    }
});
