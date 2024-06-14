namespace QingYi.AXML.TypedValue

type TypedValue =
    { mutable typeValue : int
      mutable stringValue : string
      mutable dataValue : int
      mutable assetCookieValue : int
      mutable resourceIdValue : int
      mutable changingConfigurationsValue : int }

module TypedValue =
    let TYPE_NULL = 0
    let TYPE_REFERENCE = 1
    let TYPE_ATTRIBUTE = 2
    let TYPE_STRING = 3
    let TYPE_FLOAT = 4
    let TYPE_DIMENSION = 5
    let TYPE_FRACTION = 6
    let TYPE_FIRST_INT = 16
    let TYPE_INT_DEC = 16
    let TYPE_INT_HEX = 17
    let TYPE_INT_BOOLEAN = 18
    let TYPE_FIRST_COLOR_INT = 28
    let TYPE_INT_COLOR_ARGB8 = 28
    let TYPE_INT_COLOR_RGB8 = 29
    let TYPE_INT_COLOR_ARGB4 = 30
    let TYPE_INT_COLOR_RGB4 = 31
    let TYPE_LAST_COLOR_INT = 31
    let TYPE_LAST_INT = 31

    let COMPLEX_UNIT_PX = 0
    let COMPLEX_UNIT_DIP = 1
    let COMPLEX_UNIT_SP = 2
    let COMPLEX_UNIT_PT = 3
    let COMPLEX_UNIT_IN = 4
    let COMPLEX_UNIT_MM = 5
    let COMPLEX_UNIT_SHIFT = 0
    let COMPLEX_UNIT_MASK = 15
    let COMPLEX_UNIT_FRACTION = 0
    let COMPLEX_UNIT_FRACTION_PARENT = 1
    let COMPLEX_RADIX_23p0 = 0
    let COMPLEX_RADIX_16p7 = 1
    let COMPLEX_RADIX_8p15 = 2
    let COMPLEX_RADIX_0p23 = 3
    let COMPLEX_RADIX_SHIFT = 4
    let COMPLEX_RADIX_MASK = 3
    let COMPLEX_MANTISSA_SHIFT = 8
    let COMPLEX_MANTISSA_MASK = 0xFFFFFF

