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

lorem ipsum

### 2. Eren Bot
- Greedy by Avoiding Wall Collision: Eren bot akan memprioritaskan untuk menghindari tabrakan dengan dinding. Algoritma ini memungkinkan bot bergerak secara optimal untuk menghindari kerusakan yang disebabkan oleh tabrakan.
- Greedy by Shot Velocity: Eren bot akan Eren bot akan menyesuaikan kecepatan dan kekuatan tembakan berdasarkan jarak musuh yang dipindai oleh radar. Bot ini berfokus pada kecepatan tembakan untuk memastikan bahwa tembakan mengenai target dengan efektif meskipun bot bergerak cepat.

### 3. Mikasa Bot
- Greedy by Shot Accuration: Mikasa Bot berfokus pada akurasi tembakan, terutama ketika musuh berada pada jarak dekat. Bot ini akan menabrak dan menembak musuh dengan kekuatan peluru maksimal jika musuh berada pada jarak dengan tingkat akurasi tinggi.
- Greedy by Enemy's Energy: Mikasa Bot memprioritaskan untuk menyerang musuh berdasarkan energi yang tersisa pada musuh. Jika energi musuh tinggi, bot akan menyerang dengan kekuatan penuh untuk memaksimalkan damage yang diberikan.

### 4. Armin Bot
- Greedy by Shot Power: Armin bot akan memilih tembakan dengan daya yang besar tanpa memperhatikan akurasi. Bot ini selalu menembak musuh dengan peluru maksimal dengan harapan dapat memaksimalkan kerusakan pada bot musuh jika mengenai bot lawan.
- Greedy by Shot Amount: Armin bot memprioritaskan jumlah tembakan yang lebih banyak. Bot akan menembak setiap kali meriam siap sehingga jumlah tembakan maksimum dengan harapan dapat mengenai lawan sebanyak mungkin meskipun tidak melakukan prediksi posisi atau pola pergerakan lawan.

## Struktur
```bash
├── doc
│   └── Laporan_EREMIKA.pdf
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
│   │   ├── Levi
│   │   │    ├── Levi.cmd
│   │   │    ├── Levi.cs
│   │   │    ├── Levi.csproj
│   │   │    ├── Levi.json
│   │   │    └── Levi.sh
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
foto tampilan setelah berhasil
