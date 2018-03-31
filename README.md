# IoT-Hastfest-Qingdao
IoT Hastfest @Qingdao


# 挑战1

## 用户背景

Wingtip Toys 希望借助物联网解决方或产品案帮助他们更了解来到游乐场客人的数量以及更准确的收集客户行为信息。不过在正式开发部署之前，他们需要做一系列的技术验证来证明解决方案的可行性。在这个验证程序中，通过模拟一个门禁设备（旋转栅门）来记录进入公园的游客数量，来完成对用户流量的跟踪。

在您团队中的每一名队员都会模拟成一个公园入口的管理员，这就意味着您团队中的每一位成员都需要负责创建一个模拟的门禁并保证可以正常工作，并且需要在一个客户端程序中可以监控到前端数据流，该解决方案需要支持通过设备唯一标识管理每个设备。在当前课题中我们需要在模拟在每一名游客进入公园的时候会经过旋转门并且我们需要在游客经过旋转门的时候报告一个事件。并且 Wingtip Toys 希望所提供的解决方案是可以根据游客的流量或公园扩建后门禁数量和游客的增加进行扩展，目前需要模拟数据在每秒钟报告一位客人经过旋转门。

## 技术细节

每个发送到共享中心的事件消息都需要被格式化为JSON。并且每个消息体中都需要一个全局的唯一标识符（GUID），也就是说这个唯一的标识符对应一位游客。另外模拟入口的时间戳也必须被记录。JSON的格式如下：

``` javascript
{
    "ticketId": {{uniqueIdentifier}},
    "entryTime": {{currentTime}}
}
```

## 成功条件

- 您的团队需要将服务的部署环境定位到美国西部

- 团队统一配置并指定到一个消息中心。虽然您团队中的每一位成员都可以创建一个中心，用于体验中心创建和配置过程，但是最终当这个挑战结束时所有的模拟器都应该将消息发送到团队的统一的消息中心中来。

- 在这个挑战中需要考虑到今后的负载能力所以也需要您的团队展示如何应对负载问题的挑战。

- 您团队的消息 hub 需要随着负载压力的增加而增大。

- 您的每一位团队成员都需要一个模拟器

- 您的团队必须向教练证明您的上传信息可以被实时监控。
  - 您所有的团队成员终端模拟设备需要同时运行
  - 每一位团队成员需要做一个附加项即发送一个与其他团队成员消息一致的信息到云端


## 必要条件

- IDE 环境或代码编辑器
- 团队的Azure订阅账号


## 参考资料

- **请先读这里**
  - [Azure IoT 中心与 Azure 事件中心的比较](https://docs.microsoft.com/zh-cn/azure/iot-hub/iot-hub-compare-event-hubs). 注：更多信息请参见支持信息部分。

- **Azure IoT**
  - [Azure 和物联网](https://docs.microsoft.com/zh-cn/azure/iot-hub/iot-hub-what-is-azure-iot) 在 Azure 中创建IoT解决方案。
  - [Azure IoT 中心服务概述](https://docs.microsoft.com/zh-cn/azure/iot-hub/iot-hub-what-is-iot-hub) 如何开始使用 [using IoT Hub](https://docs.microsoft.com/zh-cn/azure/iot-hub/iot-hub-get-started-simulated). 在资源列表部分您可以找到各种监视和诊断工具。


  ## 支持信息

- 阅读更多关于 [在 Azure 中选择实时消息引入技术](https://docs.microsoft.com/zh-cn/azure/architecture/data-guide/technology-choices/real-time-ingestion)

- 虽然IoT Hub 和 Event Hub 的结点是兼容的 [partitioning / 分区](https://docs.microsoft.com/zh-cn/azure/event-hubs/event-hubs-features#partitions)，并且这里我们不限定单一的成功标准，在这里多了解一些最佳实践也是对您的团队非常有帮助的。请阅读 - [Event Hubs - 最佳实践](https://docs.microsoft.com/azure/event-hubs/event-hubs-faq#best-practices)。如果有疑问请和我们的同事一起讨论。

- [IoT 中心配额和限制](https://docs.microsoft.com/zh-cn/azure/iot-hub/iot-hub-devguide-quotas-throttling)

- 并不是所有的IoT Hub 都可以进行扩展. 请您阅读 [IoT 中心 定价](https://azure.microsoft.com/pricing/details/iot-hub/).


#

# 挑战2

## 用户背景

在成功完成试点项目以后，Wingtip Toys 的IT人员开始了将智能旋转门部署到公园入口。然而，随着云霄飞车在运行过程中多次出现小问题随之成为了人们关注的焦点。据报道，出现了各种异常现象，如照片设备数据故障和列车本身故障。游乐场的管理层认为过山车的口碑对公园至关重要，所以现要求组织所有技术资源都必须收集云霄飞车的数据随后进行分析问题来源。

云霄飞车上已经部署过一些传感器设备可以收集到收集大量数据其中包括，包括速度、加速度、乘客数量以及每次运行的其他关键事件数据。因为目前生成的数据保存在列车本地,Wingtip Toys 想调查使用 IoT Hub 来将数据上传到云端进行处理数据和管理分析。在云霄飞车上总共有五列火车。

为了尽可能快地部署一个试点项目，管理层希望将 Azure IoT Hub 作为一个概念的证明，只在一列火车上使用。请您的团队成员用代码模拟五列车的数据然后配置IoT Hub连接字符串，然后将这些列车发送到云端 Azure IoT hub。

因为 Wingtip Toys 不确定他们以后要分析的数据，所以他们已经要求存储设备产生的所有数据。另外该公司也意识到，能够实时的处理数据也是非常关键的，所以他们还希望部署一个流处理引擎来处理接收到的数据并使用可视化工具展示出数据。

## 技术细节

- 您的团队需要模拟五辆过云霄飞车同时运行时所产生的数据并且将数据上传到IoT Hub。

- 所上传的数据格式需要符合JSON格式标准

- 云霄飞车所产生的数据必须包括三个传感器 GPS，加速器，事件捕捉器

- GPS data

``` javascript
{
    "rideId": "2327F177-8079-43E1-BA50-569455E2FADD",
    "trainId": "6FA812B3-FAE8-4875-8D80-62B037CD3528",
    "correlationId": "9C67DC77-F1BA-4BFA-8F38-0F76F3D5EC58",
    "lat": "44.8547",
    "long": "-93.2428",
    "alt": "246.509",
    "speed": "4.37",
    "vertAccuracy": "4",
    "horizAccuracy": "10",
    "deviceTime": "2017-12-06T20:23:43.9790000Z"
}
```

  - Accelerometer
  
``` javascript
{
    "rideId": "61397CA0-89ED-4F8C-8997-86F32AEEBD2E",
    "trainId": "05D8569B-69F9-40C8-B862-E197F9F0331E",
    "correlationId": "BB72B77A-687D-4809-92B0-407EA3633B3C",
    "accelX": "0.701859",
    "accelY": "2.19725",
    "accelZ": "1.26033",
    "deviceTime": "2018-01-17T19:06:08.0490000Z"
}
```

  - Events - 注意: 这里有几种事件类型会触发 eventType: RideStart, RideEnd, LiftStart, LiftEnd, and PhotoTriggered
  
``` javascript
{
    "rideId": "61397CA0-89ED-4F8C-8997-86F32AEEBD2E",
    "trainId": "05D8569B-69F9-40C8-B862-E197F9F0331E",
    "correlationId": "BB72B77A-687D-4809-92B0-407EA3633B3C",
    "passengerCount": "30",
    "eventType": "LiftStart",
    "deviceTime": "2018-01-17T19:06:08.7490000Z"
}
```

- 解决方案中数据到达缓冲器中以后必须要在两个小时以内到达存储器内。
- 数据展示需要在一个可视化页面中展示并且数据展示的时间延迟不能大于5分钟。

## 成功标准

- **模拟数据上传到 IoT Hub**
  - 您的团队需要将服务的部署环境定位到美国西部
  - 您需要展示模拟上传的数据中包含三个传感器的数值，GPS，加速器，事件捕捉器，其中事件捕捉器应该有多重事件类型会触发
  - 您的团队需要展示模拟5列云霄飞车一起行驶后产生数据并且上传到IoT Hub

- **Stream Processing / 流分析**
  - 您团队的流处理环境需要部署到正确的区域(请您询问微软同事检查正确的区域)
  - 您团队已经在Azure中部署了所选的流处理引擎。
  - 您团队已经将流处理引擎连接到团队的IoT Hub，并展示他们能够读取数据。

- **Data Archival / 数据归档**

  - 您的团队需要将数据保存到Azure订阅中，并为其创建一个存储帐户中的容器中。
  - 您的团队应提供一个数据归档解决方案，并提供一个带有数据的Azure Blob存储容器。
  - Azure Blob存储容器中的数据应该按照年、月、日的颗粒度度进行划分存储。
  - 数据分区应该在消息内报告的时间戳上进行，而不是消息到达缓冲摄入点的时间。
  - 如果选择的数据存档工具支持它，则数据保存到存储中应该被压缩。

- **生成报告**

  - 您的可视化报告必须符合以下条件：
    - 处理输入的实时数据流，而不是blob存储中提供的静态测试数据。
    - 在处理数据时，解释迟到和无序事件的原因 — 为迟到的事件假设一个5分钟的窗口。
    - 对于每一个云霄飞车，用5分钟为单位显示发车的次数和乘坐的人数。
    - 可视化报告必须基于消息内的时间戳计算，而不是消息到IoT Hub的时间。

  - 您的团队需要成功演示流处理作业，该作业将查询结果发送到团队所选择的持久性存储。输出必须包含以下信息:
    - 云霄飞车(ride)标识符。
    - 云霄飞车的启动时间。
    - 云霄飞车的启动次数。
    - 乘坐过山车的乘客总数。

  - 您的团队需要演示显示持久查询数据的可视化或图表。
    

## 提示信息

- 更多的云霄飞车 = 更多的数据 = 需要更多 IoT Hub 承载力.

- 有一些文档在HDInsight中写的关于Spark，这些文档可以和Azure中的Databricks一起使用(有时甚至不需要修改)。如果在Databricks文档中找不到什么东西，那么请在HDInsight上找找类似的内容。

- 对于Spark Structured streaming，数据从IoT Hub传输进来，存储在“body” column中，并且IoT Hub / Event Hub 默认会将数据采用二进制格式进行传输。您的团队应该将二进制数据转换成字符串格式，以便能够使用它。

- 流处理引擎可能需要一点时间才能连接到流并开始使用数据。如果时间超过5分钟还没有数据呈现那可能是哪里出现了问题。

- 不要忘记消息有课能delay需要处理。

- Spark结构化流支持在数据上编写SQL查询，比如“Spark”。sql(' SELECT * FROM myTable ')'

- Apache Hive样式的SQL语言也是可用的，虽然不是所有的函数都支持。

- 当使用Spark Structured streaming时，只有在使用Java或Scala时，才能完成这些挑战。另外在Python中完成这一挑战也是可能的，只是需要一个额外的处理步骤。

## 参考资料

- **请先读这里**
  - [在 Azure 中选择流处理技术](https://docs.microsoft.com/zh-cn/azure/architecture/data-guide/technology-choices/stream-processing)
  - [使用者组](https://docs.microsoft.com/zh-cn/azure/event-hubs/event-hubs-features#consumer-groups)
  - [A Gentle Introduction to Apache Spark on Databricks](https://docs.azuredatabricks.net/spark/latest/gentle-introduction/gentle-intro.html#azure-gentle-introduction-to-apache-spark-azure)
  - [Spark Structured Streaming Programming Guide](http://spark.apache.org/docs/latest/structured-streaming-programming-guide.html)

- **流处理 / Stream Processing  & 数据存储 - Spark / Data Storage - Spark**
  - [Working with Complex Data Formats with Structured Streaming in Apache Spark 2.1](https://databricks.com/blog/2017/02/23/working-complex-data-formats-structured-streaming-apache-spark-2-1.html) - Good information on dealing with converting from JSON to a tabular format.
  - [Apache Spark Structured Streaming on HDInsight to process events from Event Hubs](https://docs.microsoft.com/azure/hdinsight/spark/apache-spark-eventhub-structured-streaming#run-spark-shell-on-your-hdinsight-cluster)
  - [Access Azure Blob Storage from Azure Databricks](https://docs.databricks.com/spark/latest/data-sources/azure/azure-storage.html)
  - [Using Spark on Azure Databricks to consume data from EventHubs](https://lenadroid.github.io/posts/connecting-spark-and-eventhubs.html)

- **流处理 / Stream Processing & 数据存储与流分析 / Data Storage - Azure Stream Analytics**
  - [什么是流分析？](https://docs.microsoft.com/zh-cn/azure/stream-analytics/stream-analytics-introduction)
  - [数据连接：了解从事件到流分析的数据流输入](https://docs.microsoft.com/zh-cn/azure/stream-analytics/stream-analytics-define-inputs)
  - [流分析输出：存储、分析选项](https://docs.microsoft.com/zh-cn/azure/stream-analytics/stream-analytics-define-outputs)


## 支持信息

- 如果Spark是您团队选择的流处理环境，则需要一个事件集线器连接器连接到数据流。该连接器的Maven坐标为:
  - Azure Databricks: [com.microsoft.azure:azure-eventhubs-databricks_2.11:3.4.0](http://search.maven.org/#artifactdetails%7Ccom.microsoft.azure%7Cazure-eventhubs-databricks_2.11%7C3.4.0%7Cjar)
  - HDInsight: [com.microsoft.azure:azure-eventhubs-spark_2.11:2.1.6](http://search.maven.org/#artifactdetails%7Ccom.microsoft.azure%7Cazure-eventhubs-spark_2.11%7C2.1.6%7Cjar)

- 理解如何 [监视 Azure IoT 中心的运行状况并快速诊断问题](https://docs.microsoft.com/zh-cn/azure/iot-hub/iot-hub-monitor-resource-health).

- 理解 Azure 中的 - [HDInsight](https://docs.microsoft.com/azure/hdinsight/spark/apache-spark-overview) 和 [Azure Databricks](https://docs.azuredatabricks.net)

- 常见问题 [PySpark SQL Functions](http://spark.apache.org/docs/latest/api/python/pyspark.sql.html) & [Scala Spark SQL Column Functions](http://spark.apache.org/docs/latest/api/scala/index.html#org.apache.spark.sql.Column)

- Python 介绍 [Beginner's Guide to Python](https://wiki.python.org/moin/BeginnersGuide)

- Scala 介绍 [Tour of Scala](http://docs.scala-lang.org/tour/tour-of-scala.html)

- 理解如何加载外部程序集在 [HDInsight](https://docs.microsoft.com/azure/hdinsight/spark/apache-spark-jupyter-notebook-use-external-packages) 和 [Azure Databricks](https://docs.azuredatabricks.net/user-guide/libraries.html)

- [Why Would I Ever Need to Partition My Big ‘Raw’ Data?](https://www.red-gate.com/simple-talk/cloud/cloud-data/ever-need-partition-big-raw-data/)

- [Hadoop Azure Support: Azure Blob Storage](https://hadoop.apache.org/docs/current/hadoop-azure/index.html).
