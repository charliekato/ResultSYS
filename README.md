# ResultSYS

## ResultSYS (ResultMonitor)
セイコーの競泳リザルトシステム v5 のデータベースを読み込んでレーン順を表示するプロクラム

![スクリーンショット 2023-12-07 222112](https://github.com/charliekato/ResultSYS/assets/122329903/21312b27-dd70-4602-b850-058f1e1f1aff)

*スクリーンショット*


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

### mkReadme
セイコーリザルトシステムのデータベースのdirectoryに移動してmkreadme とタイプする。
mkreadme > readme.txt とやれば、 各mdbはどの大会の物かがわかるreadme.txtを作ってくれる。
![スクリーンショット 2023-12-07 211451](https://github.com/charliekato/csharp/assets/122329903/4f8dfed6-dfa9-4964-94a3-2470706148e8)

### chkcomport
セイコーリザルトシステムでオンラインの設定を行う際にどの通信ポートにすればよいか調べるときに使う。ダブルクリック もしくはコマンドラインから chkComportとタイプ。


![スクリーンショット 2023-12-06 070934](https://github.com/charliekato/csharp/assets/122329903/1e593826-8ffb-46fa-973a-bbb5a0eaf0f8)
