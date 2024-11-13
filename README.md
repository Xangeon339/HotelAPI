# HotelAPI
Proje Tanımı

HotelAPI, basit bir otel rehberi projesidir. Bu API aracılığıyla oteller hakkında CRUD işlemleri (oluşturma, okuma, silme) yapabilir, otellerin iletişim bilgilerini yönetebilir ve konum bazlı istatistiksel raporlar oluşturabilirsiniz.

Kurulum

RabbitMQ Kurulumu:
Proje, mesaj kuyruğu sistemi olarak RabbitMQ kullanmaktadır. RabbitMQ'yu Docker ile çalıştırmak için aşağıdaki komutu kullanın:

docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4.0-management


RabbitMQ yönetim arayüzüne http://localhost:15672 adresinden ulaşabilirsiniz. Kullanıcı adı: guest, şifre: guest.

Veri Modeli

ContactInformation:
InformationType: İletişim bilgisinin türü (örn: Telefon, Email, Web sitesi)
Location: İletişim bilgisinin ait olduğu konum (örn: Şehir, Ülke)
InformationContent: İletişim bilgisinin değeri (örn: Telefon numarası, Email adresi)
Not: Eğer InformationType değeri "Location" ise, InformationContent değeri bir Location nesnesi olarak JSON formatında saklanmalıdır.
İşlevsellikler

Otel İşlemleri:
Otel oluşturma
Otel kaldırma
Otel iletişim bilgisi ekleme
Otel iletişim bilgisi kaldırma
Raporlama:
Otel yetkililerinin listelenmesi
Otel detay bilgileri (iletişim bilgileri dahil)
Konum bazlı otel istatistikleri (otel sayısı, telefon numarası sayısı)
Oluşturulan raporların listesi
Rapor detayları
Rapor İçeriği

Oluşturulan raporlar aşağıdaki bilgileri içerir:

Konum Bilgisi: Raporun oluşturulduğu coğrafi konum
Otel Sayısı: Belirtilen konumda kayıtlı otel sayısı
Telefon Numarası Sayısı: Belirtilen konumda kayıtlı tüm otel telefon numaralarının toplamı