# nopCommerce-SqlCe-Patch
>nopCommerce Plugin to patch some database functions for Sql Compact.

The limitations of Sql Compact mean using it in production isn't great - it is good enough during development, but at any point of failure in the system Sql Compact itself may make it fail for a second time producing a second unnecessary error, hiding the first. This plugin attempts to bypass that by just throwing away the Sql Compact error and continuing with displaying the first, which may or may not be helpful to developers.
