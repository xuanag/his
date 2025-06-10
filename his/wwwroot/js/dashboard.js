// Mock JSON data
const dashboardData = {
    totalSamples: 289,
    totalPatients: 209,
    growthRate: 12.5,
    revenue: 520000000,
    expenses: 310000000,
    testTypes: [
        { name: "SH", value: 210 },
        { name: "VSKS", value: 59 },
        { name: "ELISA", value: 10 },
        { name: "GPB", value: 5 },
        { name: "HH", value: 3 },
        { name: "SHPT", value: 2 }
    ],
    monthlyRevenue: [120, 130, 170, 250, 220, 180, 270, 300, 290, 340, 360, 400], // triệu VND
    tatByMonth: [45, 40, 38, 36, 35, 34, 32, 30, 28, 27, 26, 25], // minutes
    cosByMonth: [11000000, 9500000, 7000000, 4500000, 6000000]
};

// Update cards
document.getElementById('totalSamples').textContent = dashboardData.totalSamples;
//document.getElementById('totalPatients').textContent = dashboardData.totalPatients;
document.getElementById('growthRate').textContent = dashboardData.growthRate + '%';
document.getElementById('revenue').textContent = dashboardData.revenue.toLocaleString('vi-VN') + ' đ';
document.getElementById('expenses').textContent = dashboardData.expenses.toLocaleString('vi-VN') + ' đ';

// Donut Chart (Test Type Distribution)
new Chart(document.getElementById('donutChart'), {
    type: 'doughnut',
    data: {
        labels: dashboardData.testTypes.map(t => t.name),
        datasets: [{
            data: dashboardData.testTypes.map(t => t.value),
            backgroundColor: ['#4caf50', '#e91e63', '#2196f3', '#ff9800', '#9c27b0', '#00bcd4']
        }]
    },
    options: {
        plugins: {
            title: {
                display: true,
                text: 'Tỷ lệ loại xét nghiệm'
            }
        }
    }
});

// Bar Chart (Monthly Revenue)
new Chart(document.getElementById('barChart'), {
    type: 'bar',
    data: {
        labels: ['Th1', 'Th2', 'Th3', 'Th4', 'Th5', 'Th6', 'Th7', 'Th8', 'Th9', 'Th10', 'Th11', 'Th12'],
        datasets: [{
            label: 'Doanh thu (triệu VND)',
            data: dashboardData.monthlyRevenue,
            backgroundColor: '#42a5f5'
        }]
    },
    options: {
        plugins: {
            title: {
                display: true,
                text: 'Doanh thu theo tháng'
            }
        },
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
});

// Line Chart - TAT
new Chart(document.getElementById('tatChart'), {
    type: 'line',
    data: {
        labels: ['Th1', 'Th2', 'Th3', 'Th4', 'Th5', 'Th6', 'Th7', 'Th8', 'Th9', 'Th10', 'Th11', 'Th12'],
        datasets: [{
            label: 'TAT trung bình (phút)',
            data: dashboardData.tatByMonth,
            fill: false,
            borderColor: '#ff5722',
            tension: 0.4
        }]
    },
    options: {
        plugins: {
            title: {
                display: true,
                text: 'Thời gian trung bình trả kết quả (TAT)'
            }
        },
        scales: {
            y: {
                beginAtZero: true,
                title: {
                    display: true,
                    text: 'Phút'
                }
            }
        }
    }
});

// Line Chart - Chi phi theo nhom may
new Chart(document.getElementById('cosChart'), {
    type: 'bar', // Hoặc 'pie' nếu bạn muốn biểu đồ tròn
    data: {
        labels: ['Máy Sinh Hóa', 'Máy Miễn Dịch', 'Máy Huyết Học', 'Máy Nước Tiểu', 'Máy Đông Máu'],
        datasets: [{
            label: 'Chi phí (VND)',
            data: dashboardData.cosByMonth,
            backgroundColor: [
                '#4e73df',
                '#1cc88a',
                '#36b9cc',
                '#f6c23e',
                '#e74a3b'
            ],
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: {
                display: false
            },
            title: {
                display: true,
                text: 'Chi phí theo từng nhóm máy'
            }
        },
        scales: {
            y: {
                beginAtZero: true,
                ticks: {
                    callback: function (value) {
                        return value.toLocaleString() + ' đ';
                    }
                }
            }
        }
    }
});