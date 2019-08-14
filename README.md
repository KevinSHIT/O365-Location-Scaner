# **Office365数据所属地区扫描器** by Kevin

## 使用

工具使用3个文本进行工作，分别为

```
account.txt    //用于存储需要扫描的O365账号
result.txt     //用于存储结果，可以使用Excel打开，以空格切割
error.txt      //出现错误的账号存储
```
这里需要注意的是account.txt必须存储的为xxx@xxx.onmicrosoft.com，否则会被丢弃入错误账号的区块。

## 感谢

**Vicer**提供的API  
**🍉西瓜**的测试

### 更新日志
| 日期 | 版本 | 内容 |
| -- | --| -- |
| 20190815 | 0.0.1-test | 第一个测试版本 |
| 20190815 | 0.0.2-test | 增强容错性 |
| 20190815 | 0.0.3-beta | 提升效率 |
| 20100815 | 0.0.4-beta | 增加写入文件功能 |
| 20190815 | 0.0.5-beta | 修复无限循环的逻辑错误|
| 20190815 | 0.0.6-beta | 使Excel分割更清晰 |