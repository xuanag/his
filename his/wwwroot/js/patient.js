function openModalTiepNhan() {
    document.getElementById('modalTiepNhan').style.display = 'flex';
    autoFill();
}

function closeModalTiepNhan() {
    document.getElementById('modalTiepNhan').style.display = 'none';
}

function autoFill() {
    const firstNames = ["Nguyễn", "Trần", "Lê", "Phạm", "Hoàng"];
    const middleNames = ["Văn", "Thị", "Hữu", "Thế", "Minh"];
    const lastNames = ["Hải", "Linh", "Dũng", "Trang", "Tuấn", "Lan"];
    const addresses = ["Hà Nội", "TP. HCM", "Đà Nẵng", "Huế", "Cần Thơ", "Bình Dương"];

    const gender = Math.random() < 0.5 ? "Nam" : "Nữ";

    const fullName = `${firstNames[Math.floor(Math.random() * firstNames.length)]} ${middleNames[Math.floor(Math.random() * middleNames.length)]} ${lastNames[Math.floor(Math.random() * lastNames.length)]}`;
    const phone = "09" + Math.floor(100000000 + Math.random() * 900000000).toString().substring(0, 8);
    const cccd = '0' + Array.from({ length: 11 }, () => Math.floor(Math.random() * 10)).join('');

    const now = new Date();
    const birthDate = new Date(now.getFullYear() - Math.floor(Math.random() * 70 + 18), Math.floor(Math.random() * 12), Math.floor(Math.random() * 28 + 1));
    const issueDate = new Date(birthDate.getFullYear() + 18 + Math.floor(Math.random() * 10), Math.floor(Math.random() * 12), Math.floor(Math.random() * 28 + 1));

    const chuanDoans = [
        "Viêm phổi", "Tăng huyết áp", "Tiểu đường", "Rối loạn tiêu hoá",
        "Suy thận", "Đột quỵ", "Sốt xuất huyết", "Viêm gan B", "Covid-19", "Viêm ruột thừa"
    ];

    const khoaNhapViens = [
        "KB", "23", "25", "26", "28", "16", "17", "20"
    ];
    const chuanDoan = chuanDoans[Math.floor(Math.random() * chuanDoans.length)];
    const khoa = khoaNhapViens[Math.floor(Math.random() * khoaNhapViens.length)];

    document.getElementById("FullName").value = fullName;
    document.getElementById("Gender").value = gender;
    document.getElementById("DateOfBirth").value = birthDate.toISOString().split("T")[0];
    document.getElementById("IdCardNo").value = cccd;
    document.getElementById("IdCardDate").value = issueDate.toISOString().split("T")[0];
    document.getElementById("Phone").value = phone;
    document.getElementById("Address").value = addresses[Math.floor(Math.random() * addresses.length)];
    document.getElementById("Reason").value = chuanDoan;
    document.getElementById("DepartmentCode").value = khoa;
}

document.addEventListener('keydown', function (e) {
    if ((e.ctrlKey || e.metaKey) && e.key.toLowerCase() === 's') {
        const modal = document.getElementById('modalTiepNhan');
        if (modal && modal.style.display !== 'none') {
            e.preventDefault();
            document.getElementById('patientForm').requestSubmit();
        }
    }
});

document.getElementById('patientForm').addEventListener('submit', async function (e) {
    const deptSelect = document.getElementById('DepartmentCode');
    const deptNameInput = document.getElementById('DepartmentName');
    deptNameInput.value = deptSelect.options[deptSelect.selectedIndex].text;

    const emrSelect = document.getElementById('EmrTypeCode');
    const emrNameInput = document.getElementById('EmrTypeName');
    emrNameInput.value = emrSelect.options[emrSelect.selectedIndex].text;

    e.preventDefault();

    const formData = new FormData(e.target);
    const patient = Object.fromEntries(formData.entries());

    // Gửi yêu cầu AJAX
    fetch('/patients/create', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(patient)
    })
        .then(response => response.json())
        .then(data => {
            // Kiểm tra phản hồi từ server
            if (data.redirectUrl) {
                // Redirect đến trang mới sau khi thêm thành công
                window.location.href = data.redirectUrl;
            } else {
                // Nếu không có URL redirect, bạn có thể xử lý lỗi hoặc hiển thị thông báo
                alert('Có lỗi xảy ra: ' + (data.message || 'Không xác định'));
            }
        })
        .catch(error => {
            console.error('Lỗi:', error);
        });
});

function openModalXetNghiem() {
    document.getElementById('modalXetNghiem').style.display = 'flex';
}

function closeModalXetNghiem() {
    document.getElementById('modalXetNghiem').style.display = 'none';
}

document.getElementById('xetNghiemForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const form = e.target;
    const data = new FormData(form);

    // Example: Convert FormData to object (optional)
    const payload = {};
    data.forEach((value, key) => {
        if (payload[key]) {
            payload[key] = [].concat(payload[key], value); // handle multi-select
        } else {
            payload[key] = value;
        }
    });

    console.log("Dữ liệu gửi đi:", payload);

    // Bạn có thể gửi dữ liệu này lên server bằng fetch hoặc AJAX tại đây
    alert("Đã lưu chỉ định");

    closeModal();
});