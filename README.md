# ResultSYS

## ResultSYS (ResultMonitor)
セイコーの競泳リザルトシステム v5 のデータベースを読み込んでレーン順を表示するプロクラム
プログラムを動かすためには次のファイルが必要
1. ResultMonitor.exe  (Visual studioを使ってbuildする。)
2. ResultMonitor.ini (1のexeと同じフォルダーに保存しておく)
3. swmresult.accdb (2のiniファイルに場所を指定しておく)
4. IsisCmd.txt (これはセイコーのリザルトシステムで作られるものでその場所を ResultMonitor.iniに指定しておく)
    4のIsisCmd.txtはなくても問題なく動くが、セイコーリザルトシステムで「選手紹介」とか「結果表示」を押した時にこのファイルが変更されるが、そのIsisCmd.txtの場所をResultMonitor.iniに書いておかないと適切に動かない。

## SwimDB2acc
ResultMonitor で使う swmresult.accdb を Swimxx.mdb から作るプログラム。
swmresult.accdb はapache web serverで競技結果を表示するときに run.plというcgiで使用する。

## others
others フォルダーの cs ファイルはVisual Studioではなく csc でコンパイルすれば動く。 
### acc2html
run.plで使っている　accessのdbをhtml形式に変換するプログラム。swmresult.accdbからhtml形式形式で標準出力に出力する。
otherというフォルダーにある。 visual studioではなくコマンドラインから csc でコンパイルする。


