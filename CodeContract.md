# コード規約
## ソースコード
### CamelCase
- lowerCamelCaseは最初の単語の先頭が小文字で,以後の単語の先頭は大文字,他は小文字.
- パスカルケースは単語の先頭文字が大文字.他は小文字.
***
### ファイル構成
	個人で作業する内容はすべてAssets/(各自の姓をローマ字(はじめは大文字))内で作業する.
また､Sceneは,`Assets/(自分のフォルダ)/Scenes/`フォルダ内に保存.  
Scriptは,`Assets/(自分のフォルダ)/Scripts/`フォルダ内に保存.  
のように保存する.
***
### インデント数
- 半角スペース4つ分(半角英数が4つはいるスペース)
***
### 命名規則
- メンバ変数の命名はlowerCamelCaseで命名する.
```
(例:float score ○, float Score ×)
```
- メンバ定数,メソッド,セッターゲッター,Scene名,オブジェクト名はパスカルケースで命名する.
```
(例:static float TopScore ○, static float topScore ×)
```
***
### 共通部品はPrefab化すること｡共通部品には専用Scriptをもたせること
(Prefab化は(Assets/Master/)を作っておくので(Assets/Master/Prefabs/)に保存すること.  
また,編集する際のPrefabは各自のフォルダ内で行い,編集が終わり次第,Master内に反映させるようにすること.)
***
### シーン1つにコントローラーを実装すること｡命名規則はパスカルケースで
(Scene名 + Controller)にする.
(このコントローラーオブジェクトに生成するコードやシーン遷移,
ゲーム内タイムなどのシーン全体の処理を行う)
***
### 中かっこの位置はBSD/オールマンスタイルを採用する
```
if(条件式)
{
	処理
}
```
***
### コメントの位置は処理は処理の上､メンバ変数はTabで空けて横に書く
- 処理はメソッドの上に入れる,中に分かりづらい処理があった場合は記入すること.
```
float playerScore;	//スコア変数

//加点処理
void AddScore()
{
	playerScore += 100.0f;
}
```
***
### 推奨事項
Unityの機能であるStart関数やUpdate関数内での処理は極力避けること.  
メソッドを増やして関数を飛ばして処理を行うことを推奨する.
#### NG
```
void Update()
{
	rb.AddForce(transform.forward);
}
```
#### OK
```
void Update()
{
	Move();
}

void Move()
{
	rb.AddForce(transform.forward);
}
```
***
## GitHub
### コミットメッセージはすべて英語で書くこと
- 内容 + ファイル名  
(例 : edit Player)  
(例 : add Player)  
(例 : delete Player)
***
### ブランチ名は各自の姓で先頭を大文字にすること
(例 : Miyaguni)
***