Register işlemi yapıldıktan sonra PostgreSQL üzerinden Admin rolü atanması gerekmektedir. Aksi takdirde Müşterilerin bilgilerine (CRUD işlemlerine) ulaşılamamaktadır.

1-)
Register işlemi yaptıktan ve database oluşturduktan sonra AspNetUsers tablosuna girin ve kayıt olan kullanıcının id'sini kopyalayın

![userIdTable](https://github.com/user-attachments/assets/539e400a-0c74-471f-84c8-4a07a0129643)

2-)
Daha sonra AspNetRoles tablosuna girin ve admin id'sini kopyalayın

![son](https://github.com/user-attachments/assets/e170f35c-45f7-4538-bc2d-724ac8a05ba9)

![aspRole](https://github.com/user-attachments/assets/5f31acc8-6039-4cc2-8ca7-7d84e6ab4505)


3-)
Son olarak AspNetUserRoles tablosuna girin ve kopyaladığınız user ve admin id 'lerini ilgili yere yapıştırın.

![userRoleClaim](https://github.com/user-attachments/assets/d69d3764-a481-4bad-bcd5-910cebdbab22)
![userRolecorrect](https://github.com/user-attachments/assets/76c1d200-a100-4879-94c7-56011b719927)

4-)
Artık login işlemi yaptıktan sonra müşterilerin bilgileri ve crud işlemleri görüntülenebilir.



