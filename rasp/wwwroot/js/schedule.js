const editBtn = document.getElementById('editBtn');
const saveBtn = document.getElementById('saveBtn');
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
    document.body.classList.add('edit-mode');
    saveBtn.style.display = 'inline-block';
    editBtn.style.display = 'none';
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
    span.addEventListener('click', () => {
        const card = span.closest('.group-card');
        const group = card.dataset.group;
        const day = card.closest('.day-section').dataset.day;
        console.log(`Удалить расписание группы ${group} на день ${day}`);
    });
});

saveBtn.addEventListener('click', async () => {
    const updateData = [];

    document.querySelectorAll('.group-card').forEach(card => {
        const group = card.dataset.group;
        const day = card.dataset.day;

        const lessons = [];
        card.querySelectorAll('.lesson-item').forEach(item => {
            lessons.push({
                Number: item.dataset.number,
                DisciplineCode: item.querySelector('.discipline').textContent,
                PlaceName: item.querySelector('.auditory').textContent,
                TeacherName: item.querySelector('.teacher').textContent
            });
        });

        updateData.push({
            GroupName: group,
            DayOfWeek: day,
            Lessons: lessons
        });
    });

    try {
        const resp = await fetch('@Url.Action("SaveSchedule", "RaspisAdmin")', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(updateData)
        });

        if (resp.ok) {
            alert('Расписание успешно сохранено!');
            document.body.classList.remove('edit-mode');
            saveBtn.style.display = 'none';
            editBtn.style.display = 'inline-block';
        } else {
            alert('Ошибка при сохранении');
        }
    } catch (err) {
        console.error(err);
        alert('Сетевая ошибка');
    }
});

addBtn.addEventListener('click', () => addDialog.showModal());
cancelAdd.addEventListener('click', () => addDialog.close());

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

// AJAX-submit всего дня расписания
addForm.addEventListener('submit', async e => {
    e.preventDefault();
    const formData = new FormData(addForm);
    const dayOfWeek = formData.get('DayOfWeek');
    const groupName = formData.get('GroupName');
    const DateValue = formData.get('Date');
    /*alert(DateValue);*/

    const lessons = [];
    const lessonRows = lessonsContainer.querySelectorAll('.lesson-row');

    lessonRows.forEach(row => {
        const inputs = row.querySelectorAll('input');
        lessons.push({
            Number: +inputs[0].value,
            DisciplineCode: inputs[1].value,
            PlaceName: inputs[2].value,
            TeacherName: inputs[3].value
        });
    });

    const payload = {
        Date: DateValue,
        DayOfWeek: dayOfWeek,
        GroupName: groupName,
        Lessons: lessons
    };
    try {
        const resp = await fetch(addForm.action, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });
        const json = await resp.json();
        if (json.success) {
            addDialog.close();
            window.location.reload();
        } else {
            alert('Ошибка при сохранении');
        }
    } catch (err) {
        console.error(err);
        alert('Сетевая ошибка');
    }
});