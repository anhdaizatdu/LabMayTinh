using Microsoft.AspNetCore.Mvc;

namespace LabMayTinh.Controllers
{
    public class MayTinhController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Trang Giới thiệu sinh viên & ứng dụng
        [HttpGet]
        public IActionResult GioiThieu()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PhepTinhHaiSo(string soA, string soB, string phepTinh)
        {
            ViewBag.SoA = soA;
            ViewBag.SoB = soB;
            ViewBag.PhepTinh = phepTinh;

            if (string.IsNullOrWhiteSpace(soA) || string.IsNullOrWhiteSpace(soB))
            {
                ViewBag.LoiPhepTinh = "Vui lòng nhập đầy đủ Số A và Số B!";
                return View("Index");
            }

            if (!double.TryParse(soA, out double a) || !double.TryParse(soB, out double b))
            {
                ViewBag.LoiPhepTinh = "Dữ liệu nhập vào phải là số thực hợp lệ!";
                return View("Index");
            }

            double ketQua = 0;
            switch (phepTinh)
            {
                case "+": ketQua = a + b; break;
                case "-": ketQua = a - b; break;
                case "*": ketQua = a * b; break;
                case "/":
                    if (b == 0)
                    {
                        ViewBag.LoiPhepTinh = "Không thể chia cho 0!";
                        return View("Index");
                    }
                    ketQua = a / b;
                    break;
            }

            ViewBag.KetQuaPhepTinh = $"{a} {phepTinh} {b} = {ketQua}";
            return View("Index");
        }

        [HttpPost]
        public IActionResult GiaiPTBac1(string strA, string strB)
        {
            ViewBag.PT1_A = strA;
            ViewBag.PT1_B = strB;

            if (string.IsNullOrWhiteSpace(strA) || string.IsNullOrWhiteSpace(strB))
            {
                ViewBag.LoiPT1 = "Vui lòng nhập đầy đủ hệ số a và b!";
                return View("Index");
            }

            if (!double.TryParse(strA, out double a) || !double.TryParse(strB, out double b))
            {
                ViewBag.LoiPT1 = "Hệ số a và b phải là số!";
                return View("Index");
            }

            var buocGiai = new List<string>();
            buocGiai.Add($"Phương trình dạng: {a}x + ({b}) = 0");

            if (a == 0)
            {
                if (b == 0)
                {
                    buocGiai.Add("Vì a = 0 và b = 0 => Phương trình 0x = 0 đúng với mọi x.");
                    buocGiai.Add("👉 Kết luận: Phương trình có vô số nghiệm.");
                }
                else
                {
                    buocGiai.Add($"Vì a = 0 và b = {b} => Phương trình trở thành {b} = 0 (vô lý).");
                    buocGiai.Add("👉 Kết luận: Phương trình vô nghiệm.");
                }
            }
            else
            {
                buocGiai.Add($"Bước 1: Chuyển b sang vế phải: {a}x = -({b}) = {-b}");
                double x = -b / a;
                buocGiai.Add($"Bước 2: Chia cả 2 vế cho a: x = {-b} / {a}");
                buocGiai.Add($"👉 Kết luận: Phương trình có nghiệm duy nhất x = {Math.Round(x, 4)}");
            }

            ViewBag.CacBuocPT1 = buocGiai;
            return View("Index");
        }

        [HttpPost]
        public IActionResult GiaiPTBac2(string strA, string strB, string strC)
        {
            ViewBag.PT2_A = strA;
            ViewBag.PT2_B = strB;
            ViewBag.PT2_C = strC;

            if (string.IsNullOrWhiteSpace(strA) || string.IsNullOrWhiteSpace(strB) || string.IsNullOrWhiteSpace(strC))
            {
                ViewBag.LoiPT2 = "Vui lòng nhập đầy đủ các hệ số a, b, c!";
                return View("Index");
            }

            if (!double.TryParse(strA, out double a) || !double.TryParse(strB, out double b) || !double.TryParse(strC, out double c))
            {
                ViewBag.LoiPT2 = "Các hệ số nhập vào phải là số!";
                return View("Index");
            }

            var buocGiai = new List<string>();
            buocGiai.Add($"Phương trình dạng: {a}x² + ({b})x + ({c}) = 0");

            if (a == 0)
            {
                buocGiai.Add("Vì hệ số a = 0 nên phương trình trở thành phương trình bậc 1:");
                buocGiai.Add($"{b}x + ({c}) = 0");
                if (b == 0)
                {
                    buocGiai.Add((c == 0) ? "👉 Kết luận: Phương trình vô số nghiệm." : "👉 Kết luận: Phương trình vô nghiệm.");
                }
                else
                {
                    double x = -c / b;
                    buocGiai.Add($"👉 Kết luận: Phương trình có 1 nghiệm x = {Math.Round(x, 4)}");
                }
            }
            else
            {
                double delta = b * b - 4 * a * c;
                buocGiai.Add($"Bước 1: Tính Delta: Δ = b² - 4ac = ({b})² - 4 × ({a}) × ({c}) = {delta}");

                if (delta < 0)
                {
                    buocGiai.Add("Bước 2: Vì Δ < 0 nên phương trình không có nghiệm thực.");
                    buocGiai.Add("👉 Kết luận: Phương trình vô nghiệm.");
                }
                else if (delta == 0)
                {
                    double x = -b / (2 * a);
                    buocGiai.Add("Bước 2: Vì Δ = 0 nên phương trình có nghiệm kép.");
                    buocGiai.Add($"Công thức: x₁ = x₂ = -b / (2a) = -({b}) / (2 × {a})");
                    buocGiai.Add($"👉 Kết luận: Nghiệm kép x₁ = x₂ = {Math.Round(x, 4)}");
                }
                else
                {
                    double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                    buocGiai.Add("Bước 2: Vì Δ > 0 nên phương trình có 2 nghiệm phân biệt.");
                    buocGiai.Add($"x₁ = (-b + √Δ) / (2a) = (-({b}) + √{delta}) / (2 × {a}) = {Math.Round(x1, 4)}");
                    buocGiai.Add($"x₂ = (-b - √Δ) / (2a) = (-({b}) - √{delta}) / (2 × {a}) = {Math.Round(x2, 4)}");
                    buocGiai.Add($"👉 Kết luận: PT có 2 nghiệm phân biệt: x₁ = {Math.Round(x1, 4)}, x₂ = {Math.Round(x2, 4)}");
                }
            }

            ViewBag.CacBuocPT2 = buocGiai;
            return View("Index");
        }
    }
}