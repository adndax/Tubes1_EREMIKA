# Tugas Besar 1 IF2211 Strategi Algoritma
## Pemanfaatan Algoritma Greedy dalam Pembuatan Bot Permainan Robocode Tank Royale

### Kelompok 30: EREMIKA
| Nama | NIM |
|------|-----|
| Adinda Putri | 13523071 |
| Heleni Gratia M. Tampubolon	| 13523107 |
| Naomi Risaka Sitorus | 13523122 |

## Deskripsi
Robocode Tank Royale adalah sebuah permainan simulasi robot berbasis Java. Dalam permainan ini, pemain menulis algoritma dan strategi untuk robot mereka, tetapi tidak dapat mengendalikan robot secara langsung saat pertempuran berlangsung. Sistem akan menjalankan kode yang telah ditulis untuk robot, dan robot akan bertarung di arena berdasarkan strategi yang diterapkan dalam kode tersebut. Tujuan utama dalam Robocode adalah untuk mengembangkan robot dengan algoritma yang efektif, agar robot tersebut bisa mengalahkan robot-robot lawan di arena.

Pada tugas besar 1 ini, kami mengembangkan empat bot untuk permainan Robocode Tank Royale, masing-masing dengan algoritma greedy yang memiliki heuristik berbeda. Berikut adalah deskripsi singkat dari setiap bot:

### 1. Levi Bot
- Greedy by Shot Accuration: Levi bot berfokus pada akurasi tembakan, terutama ketika musuh berada pada jarak dekat. Bot ini akan menabrak dan menembak musuh dengan kekuatan peluru maksimal jika musuh berada pada jarak dengan tingkat akurasi tinggi.
- Greedy by Enemy's Energy: Levi bot memprioritaskan untuk menyerang musuh berdasarkan energi yang tersisa pada musuh. Jika energi musuh tinggi, bot akan menyerang dengan kekuatan penuh untuk memaksimalkan damage yang diberikan. 

### 2. Eren Bot
- Greedy by Avoiding Wall Collision: Eren bot akan memprioritaskan untuk menghindari tabrakan dengan dinding. Algoritma ini memungkinkan bot bergerak secara optimal untuk menghindari kerusakan yang disebabkan oleh tabrakan.
- Greedy by Shot Velocity: Eren bot akan Eren bot akan menyesuaikan kecepatan dan kekuatan tembakan berdasarkan jarak musuh yang dipindai oleh radar. Bot ini berfokus pada kecepatan tembakan untuk memastikan bahwa tembakan mengenai target dengan efektif meskipun bot bergerak cepat.

### 3. Mikasa Bot
- Greedy by Collision Response: Mikasa bot memanfaatkan tabrakan dengan bot lawan untuk memaksimalkan serangan. Bot ini merespons terhadap tabrakan dengan melakukan pergerakan mundur dan segera menembak lawan dengan kekuatan yang disesuaikan dengan jarak lawan terhadap diri sendiri.
- Greedy by Enemy Detection: Mikasa bot akan menembak dengan kekuatan terkuat ketika mendeteksi musuh pada radarnya. Bot ini berfokus pada penyerangan dengan kekuatan maksimum demi memaksimalkan kerusakan pada bot lawan.

### 4. Armin Bot
- Greedy by Shot Power: Armin bot akan memilih tembakan dengan daya yang besar tanpa memperhatikan akurasi. Bot ini selalu menembak musuh dengan peluru maksimal dengan harapan dapat memaksimalkan kerusakan pada bot musuh jika mengenai bot lawan.
- Greedy by Shot Amount: Armin bot memprioritaskan jumlah tembakan yang lebih banyak. Bot akan menembak setiap kali meriam siap sehingga jumlah tembakan maksimum dengan harapan dapat mengenai lawan sebanyak mungkin meskipun tidak melakukan prediksi posisi atau pola pergerakan lawan.

## Struktur
```bash
├── doc
│   └── EREMIKA.pdf
├── src
│   ├── alternative-bots
│   │   ├── Armin
│   │   │    ├── Armin.cmd
│   │   │    ├── Armin.cs
│   │   │    ├── Armin.csproj
│   │   │    ├── Armin.json
│   │   │    └── Armin.sh
│   │   ├── Eren
│   │   │    ├── Eren.cmd
│   │   │    ├── Eren.cs
│   │   │    ├── Eren.csproj
│   │   │    ├── Eren.json
│   │   │    └── Eren.sh
│   │   └── Mikasa
│   │          ├── Mikasa.cmd
│   │          ├── Mikasa.cs
│   │          ├── Mikasa.csproj
│   │          ├── Mikasa.json
│   │          └── Mikasa.sh
│   └── main-bot
│       ├── Levi.cmd
│       ├── Levi.cs
│       ├── Levi.csproj
│       ├── Levi.json
│       └── Levi.sh
└── README.md
```

## Requirements
- Java Development Kit (JDK)
- .NET Core SDK 8.0
- C# Compiler/IDE
- Robocode C# API/Bridge
   
## Cara Menjalankan
#### 1. Clone Robocode Tank Royale Game Engine
```
git clone https://github.com/Ariel-HS/tubes1-if2211-starter-pack.git
```

#### 2. Masuk ke dalam directory game engine, yaitu "tank-royale-0.30.0"
```
cd tank-royale-0.30.0
```

#### 3. Jalankan file .jar aplikasi GUI
```
java -jar robocode-tankroyale-gui-0.30.0.jar
```

#### 4. Clone repository EREMIKA
Di terminal baru, jalankan perintah berikut:
```
git clone https://github.com/adndax/Tubes1_EREMIKA/
```
#### 5. Konfigurasi Booter
1. Klik tombol **"Config"** pada aplikasi GUI
2. Pilih **"Bot Root Directories"**
3. Tambahkan direktori yang berisi folder bot dari repository **EREMIKA**, yaitu:
   - `src/alternative-bots`
   - `src/main-bot`

#### 6. Menjalankan Battle
1. Klik tombol **"Battle"**
2. Klik tombol **"Start Battle"**
3. Pada panel konfigurasi permainan, pilih bot yang ingin dimainkan dari kotak kiri-atas
4. Klik tombol **"Boot →"**
5. Bot yang berhasil di-boot akan muncul di kotak kiri-bawah
6. Pilih bot yang ingin ditambahkan ke permainan dari kotak kiri-bawah
7. Klik tombol **"Add →"**
8. Bot yang telah ditambahkan akan muncul di kotak kanan-bawah
9. Mulai permainan dengan menekan tombol **"Start Battle"**



