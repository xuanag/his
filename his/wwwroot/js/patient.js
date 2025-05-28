$(document).ready(function () {
    $("#add-row").DataTable({
        pageLength: 5,
    });

    $('.btnTiepNhan').on('click', function () {
        const fakeData = randomData();
        showTiepNhanModal(fakeData);
    });

    document.querySelector('#tiepNhanModal .btn-close').addEventListener('click', function (e) {
        if (!confirm("Bạn có chắc muốn đóng?")) {
            e.stopPropagation();
        }
        else {
            // Đóng đúng modal
            const modal = bootstrap.Modal.getInstance(document.getElementById('tiepNhanModal'));
            if (modal) modal.hide();

            // Clear backdrop nếu cần
            document.querySelectorAll('.modal-backdrop').forEach(el => el.remove());
            document.body.classList.remove('modal-open');
            document.body.style = '';
        }
    });

    document.querySelector('#tiepNhanModal .btn-secondary[data-bs-dismiss="modal"]').addEventListener('click', function (e) {
        if (!confirm("Bạn có chắc muốn hủy tiếp nhận?")) {
            e.stopPropagation();
        }
        else {
            // Đóng đúng modal
            const modal = bootstrap.Modal.getInstance(document.getElementById('tiepNhanModal'));
            if (modal) modal.hide();

            // Clear backdrop nếu cần
            document.querySelectorAll('.modal-backdrop').forEach(el => el.remove());
            document.body.classList.remove('modal-open');
            document.body.style = '';
        }
    });

});

function randomData() {
    // prepare
    const firstNames = ["Nguyễn", "Trần", "Lê", "Phạm", "Hoàng"];
    const middleNames = ["Văn", "Thị", "Hữu", "Thế", "Minh"];
    const lastNames = ["Hải", "Linh", "Dũng", "Trang", "Tuấn", "Lan"];
    const addresses = ["Hà Nội", "TP. HCM", "Đà Nẵng", "Huế", "Cần Thơ", "Bình Dương"];
    const lyDo = ["Đau bụng", "Sốt cao", "Chấn thương", "Khó thở"];
    const chanDoans = [
        "Viêm phổi", "Tăng huyết áp", "Tiểu đường", "Rối loạn tiêu hoá",
        "Suy thận", "Đột quỵ", "Sốt xuất huyết", "Viêm gan B", "Covid-19", "Viêm ruột thừa"
    ];
    const khoaNhapViens = [
        "KB", "23", "25", "26", "28", "16", "17", "20"
    ];
    const maloaibas = [
        "BBO", "BNO"
    ];
    // benh nhan
    const maloaiba = maloaibas[Math.floor(Math.random() * maloaibas.length)];
    const soba = '';
    const makhoa = khoaNhapViens[Math.floor(Math.random() * khoaNhapViens.length)];
    const mabenhnhan = `BN${Math.floor(Math.random() * 90000) + 10000}`;
    const buong = '';
    const giuong = '';
    const cccdso = '0' + Array.from({ length: 11 }, () => Math.floor(Math.random() * 10)).join('');
    const hochieuso = '';
    const hoten = `${firstNames[Math.floor(Math.random() * firstNames.length)]} ${middleNames[Math.floor(Math.random() * middleNames.length)]} ${lastNames[Math.floor(Math.random() * lastNames.length)]}`;
    const ngaysinh = randomDate();
    const tuoi = '0';
    const gioitinh = Math.random() > 0.5 ? "Nam" : "Nữ";
    const diachi = addresses[Math.floor(Math.random() * addresses.length)];
    const sodienthoai = "09" + Math.floor(100000000 + Math.random() * 900000000).toString().substring(0, 8);
    const mabhyt = "BV" + Math.floor(Math.random() * 900000 + 100000).toString(); // Mã bảo hiểm y tế giả lập
    const hotennguoithan = `${firstNames[Math.floor(Math.random() * firstNames.length)]} ${middleNames[Math.floor(Math.random() * middleNames.length)]} ${lastNames[Math.floor(Math.random() * lastNames.length)]}`;
    const sodienthoainguoithan = "09" + Math.floor(100000000 + Math.random() * 900000000).toString().substring(0, 8);
    // tiep nhan
    const lydovaovien = lyDo[Math.floor(Math.random() * lyDo.length)];
    const chandoanvaovien = chanDoans[Math.floor(Math.random() * chanDoans.length)];

    return {
        maloaiba: maloaiba,
        soba: soba,
        makhoa: makhoa,
        mabenhnhan: mabenhnhan,
        buong: buong,
        giuong: giuong,
        cccdso: cccdso,
        hochieuso: hochieuso,
        hoten: hoten,
        ngaysinh: ngaysinh,
        tuoi: tuoi,
        gioitinh: gioitinh,
        diachi: diachi,
        sodienthoai: sodienthoai,
        mabhyt: mabhyt,
        hotennguoithan: hotennguoithan,
        sodienthoainguoithan: sodienthoainguoithan,
        lydovaovien: lydovaovien,
        chandoanvaovien: chandoanvaovien
    };
}

function showTiepNhanModal(data) {
    //console.log(data);
    document.getElementById("mabenhnhan").value = data.mabenhnhan;
    document.getElementById("hoten").value = data.hoten;
    document.getElementById("ngaysinh").value = data.ngaysinh;
    document.getElementById("gioitinh").value = data.gioitinh;
    document.getElementById("cccdso").value = data.cccdso;
    document.getElementById("diachi").value = data.diachi;
    document.getElementById("mabhyt").value = data.mabhyt;
    document.getElementById("hotennguoithan").value = data.hotennguoithan;
    document.getElementById("sodienthoainguoithan").value = data.sodienthoainguoithan;
    document.getElementById("makhoa").value = data.makhoa;
    document.getElementById("maloaiba").value = data.maloaiba;
    document.getElementById("lydovaovien").value = data.lydovaovien;
    document.getElementById("chandoanvaovien").value = data.chandoanvaovien;

    const modal = new bootstrap.Modal(document.getElementById('tiepNhanModal'));
    modal.show();
}

$('#formTiepNhan').on('submit', function (e) {
    e.preventDefault();

    $('#tenkhoa').val($('#makhoa option:selected').text());
    $('#loaiba').val($('#maloaiba option:selected').text());

    const formData = $(this).serialize();

    $.post('/patients/create', formData)
        .done(function (res) {
            window.location.href = res.redirectUrl; 
        })
        .fail(function (err) {
            alert('Lỗi tiếp nhận: ' + err.responseText);
        });
});

function randomDate(startYear = 1940, endYear = 2020) {
    const start = new Date(startYear, 0, 1);
    const end = new Date(endYear, 11, 31);
    const randomTime = start.getTime() + Math.random() * (end.getTime() - start.getTime());
    const randomDate = new Date(randomTime);

    const year = randomDate.getFullYear();
    const month = String(randomDate.getMonth() + 1).padStart(2, '0');
    const day = String(randomDate.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`; // yyyy-MM-dd
}