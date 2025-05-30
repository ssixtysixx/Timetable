const authBtn = document.getElementById('authBtn');
const modal = document.getElementById('authModal');
const closeBtn = document.querySelector('.close');
const dayFilter = document.getElementById('dayFilter');
const groupFilter = document.getElementById('groupFilter');

authBtn.addEventListener('click', () => {
    modal.style.display = 'block';
    document.getElementById('errorMessage').textContent = '';
});

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

dayFilter.addEventListener('change', applyFilters);
groupFilter.addEventListener('change', applyFilters);

closeBtn.addEventListener('click', () => {
    modal.style.display = 'none';
});

window.addEventListener('click', (event) => {
    if (event.target === modal) {
        modal.style.display = 'none';
    }
});

document.querySelector('.auth-form').addEventListener('submit', function (event) {
    event.preventDefault();
    const formData = new FormData(this);
    const loginBtn = document.getElementById('loginBtn');
    loginBtn.disabled = true;

    fetch('/Raspis/Login', {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                window.location.href = '/raspisadmin';
            } else {
                document.getElementById('errorMessage').textContent = data.message;
            }
        })
        .catch(error => {
            document.getElementById('errorMessage').textContent = 'Произошла ошибка при попытке входа.';
        })
        .finally(() => {
            loginBtn.disabled = false;
        });
});