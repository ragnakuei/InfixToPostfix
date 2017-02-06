Feature: InfixToPostfix
	依據 輸入的四則運算式，轉換為 PostFix 格式

Scenario Outline: 轉換為 PostFix 格式
	Given 輸入 <input>
	When 進行轉換後
	Then 結果為 <output>
	Examples: 
	| example description | input       | output  |
	| 1_add_2             | 1+2         | 12+     |
	| 1_add_2_multiple_3  | 1+2*3       | 123*+   |
	| 1_divide_2_minus_3  | 1/2-3       | 12/3-   |
	| 1_add_2_Parentheses | (1+2)       | 12+     |
	| 5                   | (1+2)*3     | 12+3*   |
	| 6                   | 1-(2/3)     | 123/-   |
	| 7                   | ((1+2))/3   | 12+3/   |
	| 8                   | ((1+2)-3)/4 | 12+3-4/ |
	| 9                   | (1+(2-3))/4 | 123-+4/ |
