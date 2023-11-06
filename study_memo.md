# ASPNETでの環境変数の読み取り方
C#ではEnviroment.GetEnviromentVariable("Name");にて取得可能。<br />
Docker composeで必要な環境変数をセットしておいて呼び出して使おうと思う。<br />

# ASPNETビューモデルの使い方(値のセット方法)
Controllerにてインスタンスを定義してあげて、その際に値を渡せばよい。<br />
また、ビューに渡すときはreturnのViewの第二引数にモデルを渡してあげる。また、これに関してはバージョンによって違うみたいなので要確認。<br />
(自分の場合、.NET7.0だったのでこれになっている。)<br />

# ASPNETのビューでのモデルの受け取り
@model PROJECT.MODEL.MYVIEWMODEL;でとれるよ<br />
また、使用するときは@Model.Nameのように使用するよ。<br />


# Dockerのコンテナ同士での通信について(主にDBについて)
コンテナ同士の接続はネットワーク設定が必須<br />
また、接続の際にipをコンテナ名にしてあげる。<br />


# C#のusingの使い方
Pythonでいうwithと同様のものと考えられる。<br />