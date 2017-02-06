Feature: InfixToPostfix
	依據 輸入的四則運算式，轉換為 PostFix 格式

Scenario Outline: 轉換為 PostFix 格式
	Given 輸入 <input>
	When 進行轉換後
	Then 結果為 <output>
	Examples: 
	| example description | input       | output  |
	| 1                   | 1+2         | 12+     |
	| 2                   | 1+2*3       | 123*+   |
	| 3                   | 1/2-3       | 12/3-   |
	| 4                   | (1+2)       | 12+     |
	| 5                   | (1+2)*3     | 12+3*   |
	| 6                   | (1/2)-3     | 12/3-   |
	| 7                   | 1*(2+3)     | 123+*   |
	| 8                   | 1-(2*3)     | 123/-   |
	| 9                   | ((1+2))/3   | 12+3/   |
	| 10                  | ((1+2)-3)/4 | 12+3-4/ |
	| 11                  | (1+(2-3))/4 | 123-+4/ |
	| 12                  | (1+2)*(1-2) | 12+12-* |